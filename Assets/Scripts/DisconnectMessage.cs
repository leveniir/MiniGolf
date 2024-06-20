using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisconnectMessage : MonoBehaviour
{
    public Image disconnectMessage;
   
    public  void ShowDisconnectMessage()
    {
        disconnectMessage.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
