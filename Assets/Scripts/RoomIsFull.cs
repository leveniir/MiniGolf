using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomIsFull : MonoBehaviour
{
    public static RoomIsFull Instance;
    public List<Button> buttons;
    void Start()
    {
        Instance = this;
    }
    public Image fullRoomMessage;
   
    public void ShowFullRoomMessage()
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
        fullRoomMessage.gameObject.SetActive(true);
    }
    public void OkayButton()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
        fullRoomMessage.gameObject.SetActive(false);

    }
}