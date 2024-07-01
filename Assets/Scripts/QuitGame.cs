using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class QuitGame : MonoBehaviour
{
    public Image exitMessage;
    public GameObject[] objs;
    private int sceneIndex;
   
    void Awake()
    {
        objs = GameObject.FindGameObjectsWithTag("Button");
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
           foreach (GameObject button in objs)
            {
                button.GetComponent<Button>().interactable = false;           
            }
           exitMessage.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void YesButton()
    {
        Application.Quit();
    }
    public void NoButton()
    {
        if (sceneIndex >= 3 && sceneIndex <= 7)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        foreach (GameObject button in objs)
        {
                button.GetComponent<Button>().interactable = true;           
        }
        exitMessage.gameObject.SetActive(false);
    }
}
