using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomIsFull : MonoBehaviour
{
    public static RoomIsFull Instance;
    public List<Button> buttons;
    public Image fullRoomMessage;
    public Image usernameMessage;
    public Image roomNotExistMessage;
    public bool showingMessage;
    public TMP_InputField username;


    void Start()
    {
        showingMessage = false;
        Instance = this;
    }   
    public void ShowFullRoomMessage()
    {
        showingMessage = true;
        foreach (var button in buttons)
        {
            button.interactable = false;
            username.interactable = false;
        }
        fullRoomMessage.gameObject.SetActive(true);
    }
    public void OkayButton()
    {
        showingMessage = false;
        foreach (var button in buttons)
        {
            button.interactable = true;

        }
        fullRoomMessage.gameObject.SetActive(false);
        roomNotExistMessage.gameObject.SetActive(false);
        usernameMessage.gameObject.SetActive(false);

    }
    public void ShowUsernameMessage()
    {
        showingMessage = true;
        foreach (var button in buttons)
        {
            button.interactable = false;
            username.interactable = false;

        }
        usernameMessage.gameObject.SetActive(true);
    }
    public void ShowNotExistRoomMessage()
        {
        showingMessage = true;
             foreach (var button in buttons)
                {
                    button.interactable = false;
                    username.interactable = false;
                }
        roomNotExistMessage.gameObject.SetActive(true); 
        }
    

}