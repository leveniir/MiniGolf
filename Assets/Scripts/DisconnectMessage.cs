using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisconnectMessage : MonoBehaviour
{
    public static DisconnectMessage Instance;
    void Awake()
    {
        Instance = this;
    }
    public Image disconnectMessage;
   
    public void ShowDisconnectMessage()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        disconnectMessage.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
