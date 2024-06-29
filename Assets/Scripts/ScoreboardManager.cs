using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    public static ScoreboardManager scoreboardManager;
    public List<TextMeshProUGUI> player1;
    public List<TextMeshProUGUI> player2;
    public List<TextMeshProUGUI> player3;
    public List<TextMeshProUGUI> player4;
    public List<TextMeshProUGUI> player5;
    public List<TextMeshProUGUI> player6;
    public List<TextMeshProUGUI> player7;
    public List<TextMeshProUGUI> player8;
    public List<List<TextMeshProUGUI>> players;

    void Awake()
    {
        // numofPlayers = funkcja() zwracanie ilosci graczy z serwera 
        // finalScore = funkcaj() zwrocenie posortowanej listy z serwera

        Dictionary<string, int[]> finalScores = new()
        {
            { Server.Username, LevelManager.Instance.levelData }
        };
        for (int i = 0; i < Server.PlayerCount; i++)
        {
            int[] scores = new int[6];
            int sum = 0;
            for (int j = 0; j < 5; j++)
            {
                int score = Server.Scores[Server.PlayerNames[i]][j];
                scores[j] = score;
                sum += score;
            }
            scores[5] = sum;
            finalScores.Add(Server.PlayerNames[i], scores);
        }
        finalScores = finalScores.OrderBy(pair => pair.Value[5]).ToDictionary(pair => pair.Key, pair => pair.Value);

        players = new List<List<TextMeshProUGUI>> { player1, player2, player3, player4, player5, player6, player7, player8 };

        DeactivateTexts();
        ActivateTexts(finalScores.Count);
        ShowScore(finalScores);
    }

    public void DeactivateTexts()
    {
        foreach (var playerList in players)
        {
            foreach (var text in playerList)
            {
                text.gameObject.SetActive(false);
            }
        }
    }
    public void ActivateTexts(int num)
    {
        for (int i = 0; i < num; i++)
        {
            foreach (var text in players[i])
            {
                text.gameObject.SetActive(true);
            }
        }
    }

    public void ShowScore(Dictionary<string, int[]> finalScores)
    {
        for (int i = 0; i < finalScores.Count; i++)
        {
            string playerName = finalScores.Keys.ToArray()[i];
            players[i][0].text = playerName;
            for (int j = 0; j < 6; j++)
            {
                players[i][j + 1].text = finalScores[playerName][j].ToString();
            }
        }
    }

}


