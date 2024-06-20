using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public TextMeshProUGUI message1;
    public TextMeshProUGUI message2;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI key;
    public Button startGame;
    public int roomType;
    public int numOfPlayers;
    public int time;
    public List<string> players;
    public List<TextMeshProUGUI> playersNames;

    void Awake()
    {   
        // numOfPlayers = funcja pobierajaca ilosc graczy z serwera 
        // roomType = Funkcja zwracajaca z serwera typ pokoju

        if (roomType == 1)
        {
            message1.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
            // time = funkcja zwracajaca czas do rozpoczecia gry 
            CountdownTimer();
        }
        else if (roomType == 2)
        {
            message2.gameObject.SetActive(true);
            key.gameObject.SetActive(true);
            startGame.gameObject.SetActive(true);
            // key.text = funkcja zwracajaca z serwera klucz do pokoju
        }
        else
        {
            message2.gameObject.SetActive(true);
            key.gameObject.SetActive(true);
        }
        ActivateTexts(numOfPlayers); // funkcja do wywolywania przez serwer za kazdym razem gdy dolaczy nowy gracz
    }

    public void ActivateTexts(int number)
    {
        // players = funkjca pobierajaca nazwy uzytkownikow z serwera

        for (int i = 0; i < number; i++)
            {
                playersNames[i].gameObject.SetActive(true);
                playersNames[i].text = players[i];
            }
    }
    IEnumerator CountdownTimer() {
        while (time> 0) { 
            timeText.text = $"{time}"; 

            yield return new WaitForSeconds(1); 
            time -= 1; 
        }
        timeText.text = "0";
        time = 0;
    }
    public void StartGame()
    {
        // wcisniecie prztcisku
        // wywolanie funkcji rozpoczecia gry dla wszytkich uzytkownikow
    }
}
