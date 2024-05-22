using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class StartSceneScript : MonoBehaviour
{
    public TMP_InputField inputPlayerName;
    public PlayerInfo player;
    public Button createUser;

    void Update()
    {
        if (inputPlayerName.text == "" || !IsNameValid(inputPlayerName.text))
        {
            createUser.interactable = false;
        }
        else
        {
            createUser.interactable = true;
        }
    }

    private bool IsNameValid(string name)
    {
        return Regex.IsMatch(name, @"^[a-zA-Z]{1,5}$");
    }

    public void ButtonCreateUser()
    {    
        player.CreatePlayer(inputPlayerName.text);
        SceneManager.LoadScene(1);
    } 
}
