using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
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
    public List<List<string>> finalScore;
    public int numOfPlayers;

    void Awake()
    {
        
        // numofPlayers = funkcja() zwracanie ilosci graczy z serwera 
        // finalScore = funkcaj() zwrocenie posortowanej listy z serwera
        
       
        players = new List<List<TextMeshProUGUI>> { player1, player2, player3, player4, player5, player6, player7, player8 };

        DeactivateTexts();
        ActivateTexts(numOfPlayers);
        ShowScore(numOfPlayers);

        
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
    public void ActivateTexts(int number)
    {
        for (int i = 0; i < number; i++)
            {
                foreach (var text in players[i])
                {
                    text.gameObject.SetActive(true);
                }
            }
    }

    public void ShowScore(int number)
    {
        for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < 7;j++)
                {
                    players[i][j].text = finalScore[i][j];
                }
            }
    }

}


