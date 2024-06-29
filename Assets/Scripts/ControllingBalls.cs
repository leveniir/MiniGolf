using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllingBalls : MonoBehaviour
{
    public static ControllingBalls Instance;
    public List<Rigidbody> balls;
    public int maxPower = 10;
    public Dictionary<string, Rigidbody> ballsNames;
    public Dictionary<string, float[]> ballsData;
    void Start()
    {
        ballsNames = new Dictionary<string, Rigidbody>();
        ballsData = new Dictionary<string, float[]>();

        // Aktywowanie odpowiedniej ilsoci pilek
        for (int i = 0; i < Server.PlayerCount; i++)
        {
            balls[i].gameObject.SetActive(true);
            ballsNames.Add(Server.PlayerNames[i], balls[i]);
        }

        // Zamiana nazwy obiektów, na nazwy graczy
        GameObject parentObject = GameObject.Find("OtherPlayers");
        if (parentObject != null)
        {
            for (int i = 0; i < Server.PlayerCount; i++)
            {
                Transform child = parentObject.transform.GetChild(i);
                child.gameObject.name = Server.PlayerNames[i];
                Server.PlayerData[Server.PlayerNames[i]] = new float[] { 0, 0, 0, child.position.x, child.position.y, child.position.z };
            }
        }
        Instance = this;
    }
    void Update()
    {
        foreach (string playerName in ballsData.Keys)
        {
            if (!Server.PlayerNames.Contains(playerName))
                PlayerDisconnected(playerName);
        }
        for (int i = 0; i < Server.PlayerCount; i++)
        {
            string playerName = Server.PlayerNames[i];
            if (!ballsData.ContainsKey(playerName) || !ballsData[playerName].SequenceEqual(Server.PlayerData[playerName]))
            {
                ballsData[playerName] = (float[])Server.PlayerData[playerName].Clone();
                PlayerHittedBall(playerName);
            }
        }
    }
    public void PlayerDisconnected(string playerName)
    {
        ballsNames[playerName].gameObject.SetActive(false);
    }
    public void PlayerHittedBall(string playerName)
    {
        ballsNames[playerName].position = new Vector3(ballsData[playerName][3], ballsData[playerName][4], ballsData[playerName][5]);
        ballsNames[playerName].velocity = Vector3.zero;
        ballsNames[playerName].angularVelocity = Vector3.zero;
        ballsNames[playerName].AddForce(new Vector3(ballsData[playerName][0], ballsData[playerName][1], ballsData[playerName][2]), ForceMode.Impulse);// odpowiednia kolejnosc przeslanych danych
    }
}
