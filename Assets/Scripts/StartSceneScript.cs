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
    public TMP_InputField privateKey;
    public PlayerInfo player;
    public Button createUser;
    public Button public_room;
    public Button private_room;
    public Button join_room;
    public Button create_room;
    public int roomType;
    public Image disconnectMessage;
    public StartSceneScript startSceneScript;

    void Awake()
    {
        roomType = 0;
        privateKey.gameObject.SetActive(false);
        HideButtons();
    }
    void Update()
    {
        if (roomType != 0 && !(inputPlayerName.text == "") && IsNameValid(inputPlayerName.text))
        {
            createUser.interactable = true;
        }
        else
        {
            createUser.interactable = false;
        }
    }

    private bool IsNameValid(string name)
    {
        return Regex.IsMatch(name, @"^[a-zA-Z]{1,5}$");
    }

    public void ButtonCreateUser()
    {    
        player.CreatePlayer(inputPlayerName.text);
        SceneManager.LoadScene(2);
    } 

    public void HideButtons()
    {
        join_room.gameObject.SetActive(false);
        create_room.gameObject.SetActive(false);
    }
    public void ShowButtons()
    {
        join_room.gameObject.SetActive(true);
        create_room.gameObject.SetActive(true);
    }
    public void PublicButton()
    {
        privateKey.gameObject.SetActive(false);
        roomType = 1;
        HideButtons();
       
    }
    public void PrivateButton()
    {
        roomType = 0;
        ShowButtons();
    }
    public void CreateRoom()
    {
        roomType = 2;
        privateKey.gameObject.SetActive(false);

    }
    public void JoinRoom()
    {
        roomType = 3;
        privateKey.gameObject.SetActive(true);

    }
}
