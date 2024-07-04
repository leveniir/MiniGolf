using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public TextMeshProUGUI message1;
    public TextMeshProUGUI message2;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI key;
    public Button startGame;
    public List<TextMeshProUGUI> playersNames;
    public List<string> players;

    void Awake()
    {
        int roomType = Server.RoomType;

        playersNames[0].gameObject.SetActive(true);
        playersNames[0].text = Server.Username;

        if (roomType == 1)
        {
            message1.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
            StartCoroutine(CountdownTimer());
        }
        else if (roomType == 2)
        {
            message2.gameObject.SetActive(true);
            key.gameObject.SetActive(true);
            key.text = $"{Server.RoomId:D2}";
            startGame.gameObject.SetActive(true);
        }
        else
        {
            message2.gameObject.SetActive(true);
            key.gameObject.SetActive(true);
            key.text = $"{Server.RoomId:D2}";
        }
    }
    void Update()
    {
        if (!players.SequenceEqual(Server.PlayerNames))
        {
            players = Server.PlayerNames;
            for (int i = 1; i < playersNames.Count; i++)
            {
                if (i <= players.Count)
                {
                    playersNames[i].gameObject.SetActive(true);
                    playersNames[i].text = players[i - 1];
                }
                else if (i > players.Count)
                {
                    playersNames[i].gameObject.SetActive(false);
                }
            }
        }
        if (Server.Disconnected)
        {
            Server.Disconnected = false;
            DisconnectMessage.Instance.ShowDisconnectMessage();
        }
        else if (Server.Level)
        {
            Server.Level = false;
            LoadNextScene();
        }
    }
    IEnumerator CountdownTimer()
    {
        long time;
        do
        {
            time = Server.Countdown - DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            timeText.text = $"{time}";

            yield return new WaitForSeconds(1);
        } while (time > 0);
        timeText.text = "0";
    }
    public void StartGame()
    {
        Server.Start();
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(3);
    }
}
