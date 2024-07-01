using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public static class Server
{
    private enum State
    {
        LOBBY,
        LEVEL_1,
        LEVEL_2,
        LEVEL_3,
        LEVEL_4,
        LEVEL_5,
        FINISH
    }
    private static State state;

    private static Socket socket;
    private static Thread thread;

    private static int roomId;
    public static int RoomId
    {
        get => roomId;
    }

    private static int roomType;
    public static int RoomType
    {
        get => roomType;
    }

    private static long countdown;
    public static long Countdown
    {
        get => countdown;
    }

    private static bool level = false;
    public static bool Level
    {
        get => level;
        set => level = value;
    }

    private static bool disconnected = false;
    public static bool Disconnected
    {
        get => disconnected;
        set => disconnected = value;
    }

    private static bool duplicate = false;
    public static bool Duplicate
    {
        get => duplicate;
        set => duplicate = value;
    }

    private static bool fullRoom = false;
    public static bool FullRoom
    {
        get => fullRoom;
        set => fullRoom = value;
    }

    private static bool invalidId = false;
    public static bool InvalidId
    {
        get => invalidId;
        set => invalidId = value;
    }

    private static Dictionary<string, float[]> players = new();
    public static int PlayerCount
    {
        get => players.Count();
    }
    public static List<string> PlayerNames
    {
        get => players.Keys.ToList();
    }
    public static Dictionary<string, float[]> PlayerData
    {
        get => players;
    }

    private static string username;
    public static string Username
    {
        get => username;
        set => username = value;
    }

    private static Dictionary<string, int[]> scores = new();
    public static Dictionary<string, int[]> Scores
    {
        get => scores;
    }

    public static bool Connect(string username, int roomId)
    {
        try
        {
            socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPAddress(new byte[] { 127, 0, 0, 1 }), 56789);
        }
        catch (SocketException)
        {
            disconnected = true;
            return false;
        }

        List<byte> data = new() { (byte)Request.Type.HELLO };
        data.InsertRange(1, BitConverter.GetBytes(IPAddress.HostToNetworkOrder(roomId)));
        data.InsertRange(5, Encoding.ASCII.GetBytes(username));
        Send(new Request(data));

        Request request = Receive();
        if (request != Request.Type.ROOM_ID)
        {
            if (request == Request.Type.INVALID && request.Data.Count > 1)
            {
                if (request.Data[1] == 0)
                {
                    fullRoom = true;
                }
                else if (request.Data[1] == 1)
                {
                    invalidId = true;
                }
                else if (request.Data[1] == 2)
                {
                    duplicate = true;
                }
            }
            return false;
        }
        Server.roomId = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(request.Data.ToArray(), 1));
        countdown = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(request.Data.ToArray(), 5));

        if (roomId == -1)
            roomType = 1;
        else if (roomId == 0)
            roomType = 2;
        else
            roomType = 3;

        state = State.LOBBY;

        Server.username = username;
        players.Clear();
        scores.Clear();

        thread?.Abort();
        thread = new Thread(Run);
        thread.Start();

        return true;
    }

    public static void BallMove(float fx, float fy, float fz, float px, float py, float pz)
    {
        List<byte> data = new() { (byte)Request.Type.MOVE };
        data.InsertRange(1, FloatToBytes(fx));
        data.InsertRange(5, FloatToBytes(fy));
        data.InsertRange(9, FloatToBytes(fz));
        data.InsertRange(13, FloatToBytes(px));
        data.InsertRange(17, FloatToBytes(py));
        data.InsertRange(21, FloatToBytes(pz));
        Send(new Request(data));
    }

    public static void Start()
    {
        Send(new Request(new List<byte>() { (byte)Request.Type.LEVEL }));
    }

    public static void Ready(int score)
    {
        Send(new Request(new List<byte>() { (byte)Request.Type.READY, (byte)score }));
    }

    public static int[] Ranking()
    {
        Request request = Receive();
        return request.Data.ToArray().ConvertTo<int[]>();
    }

    private static float BytesToFloat(byte[] b, int i)
    {
        return BitConverter.Int32BitsToSingle(IPAddress.NetworkToHostOrder(BitConverter.ToInt32(b, i)));
    }

    private static byte[] FloatToBytes(float f)
    {
        return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(BitConverter.SingleToInt32Bits(f)));
    }

    public static void Run()
    {
        while (state != State.FINISH)
        {
            Request request = Receive();
            if (request == Request.Type.HELLO)
                Send(new Request(new List<byte>() { (byte)Request.Type.OK }));
            else if (request == Request.Type.USERNAME)
            {
                string[] usernames = Encoding.ASCII.GetString(request.Data.ToArray(), 1, request.Data.Count - 1).Split('\0');
                foreach (string username in usernames)
                    if (!players.TryAdd(username, new float[6]))
                    {
                        players.Remove(username);
                        scores.Remove(username);
                    }
                    else
                        scores.Add(username, new int[5]);
            }
            else if (request == Request.Type.LEVEL)
            {
                if (state != State.LOBBY)
                {
                    byte count = request.Data[1];
                    string[] usernames = Encoding.ASCII.GetString(request.Data.ToArray(), 2 + count, request.Data.Count - 2 - count).Split('\0');
                    for (int i = 0; i < count; i++)
                        if (usernames[i] != username)
                            scores[usernames[i]][(int)state - 1] = request.Data[i + 2];
                }
                if (!level)
                    state++;
                level = true;
            }
            else if (request == Request.Type.MOVE)
            {
                byte[] data = request.Data.ToArray();
                string username = Encoding.ASCII.GetString(data, 25, data.Length - 25);
                players[username][0] = BytesToFloat(data, 1);
                players[username][1] = BytesToFloat(data, 5);
                players[username][2] = BytesToFloat(data, 9);
                players[username][3] = BytesToFloat(data, 13);
                players[username][4] = BytesToFloat(data, 17);
                players[username][5] = BytesToFloat(data, 21);
            }
        }
        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch (Exception) { }
    }

    private static Request Receive()
    {
        byte[] buffer = new byte[0];
        try
        {
            do
            {
                if (socket.Available == 0)
                    continue;
                buffer = new byte[1];
                socket.Receive(buffer);
                buffer = new byte[buffer[0]];
                socket.Receive(buffer);
            } while (buffer.Length == 0);
            return new Request(new List<byte>(buffer));
        }
        catch (SocketException)
        {
            state = State.FINISH;
            disconnected = true;
            return new Request(new List<byte>() { (byte)Request.Type.INVALID });
        }
    }

    private static void Send(Request request)
    {
        List<byte> data = request.Data;
        try
        {
            data.Insert(0, (byte)data.Count);
            socket.Send(data.ToArray());
        }
        catch (SocketException)
        {
            state = State.FINISH;
            disconnected = true;
        }
    }
}