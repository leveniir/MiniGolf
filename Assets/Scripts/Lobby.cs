using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
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
    private int roomType;
    public int numOfPlayers;
    public int time;
    public List<string> players;
    public List<TextMeshProUGUI> playersNames;
    public Lobby lobby;

    void Awake()
    { 
        // numOfPlayers = funcja pobierajaca ilosc graczy z serwera 
        // roomType = Funkcja zwracajaca z serwera typ pokoju
        //numOfPlayers = 2; test
        //ActivateTexts(2);test
        //roomType = 1;test

        if (roomType == 1)
        {
            message1.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
            startGame.gameObject.SetActive(true);
            startGame.enabled = true;
            // time = funkcja zwracajaca czas do rozpoczecia gry 
            CountdownTimer();
        }
        else if (roomType == 2)
        {
            message2.gameObject.SetActive(true);
            key.gameObject.SetActive(true);
            startGame.gameObject.SetActive(true);
            startGame.enabled = true;
            // key.text = funkcja zwracajaca z serwera klucz do pokoju
        }
        else
        {
            message2.gameObject.SetActive(true);
            key.gameObject.SetActive(true);
            startGame.gameObject.SetActive(true);
            startGame.enabled = true;
        }
        ActivateTexts(numOfPlayers); // funkcja do wywolywania przez serwer za kazdym razem gdy dolaczy nowy gracz
    }
    void Update()
    {
    
    }

    public void ActivateTexts(int number)
    {
        // players = funkjca pobierajaca nazwy uzytkownikow z serwera
        //List<string> players = new List<string> { "User1", "User2"}; test

        for (int i = 0; i < number; i++)
            {
                Debug.Log(i);
                playersNames[i].gameObject.SetActive(true);
                playersNames[i].text = players[i];
            }
    }
    IEnumerator CountdownTimer() 
    {
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
        Debug.Log("1");
        LoadNextScene();
        // wcisniecie prztcisku
        // wywolanie funkcji rozpoczecia gry dla wszytkich uzytkownikow
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(3);
    }
}
