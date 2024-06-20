using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ControllingBalls : MonoBehaviour
{
    public List<Rigidbody> balls;
    public int numOfPlayers;
    public int maxPower = 10;
    public Dictionary<string, Rigidbody> ballsNames;
    public List<string> playersNames;
    void Start()
    {
        //playersNames = funkcja zwracajaca z serwera liste z nazwami graczy
        ballsNames = new Dictionary<string, Rigidbody>();
        // numofPlayers = funkcja() zwracanie ilosci graczy z serwera 

        // Aktywowanie odpowiedniej ilsoci pilek
        for (int i = 0; i< numOfPlayers;i++)
        {
            balls[i].gameObject.SetActive(true);
        }

        // Przypisanie pilek do nazw graczy
        int index = 0;
        foreach (var player in playersNames)
        {
            if (index < balls.Count)
            {
                ballsNames.Add(player,balls[index]);
                index++;
            }
        }

        // Zamiana nazwy obiektów, na nazwy graczy
        GameObject parentObject = GameObject.Find("OtherPlayers");
        if (parentObject != null)
        {
            for (int i = 0; i < numOfPlayers; i++)
            {
                Transform child = parentObject.transform.GetChild(i);
                child.gameObject.name = playersNames[i];
            }
        }

    }
    public void PlayerDisconnected(string playerName)
    {
        ballsNames[playerName].gameObject.SetActive(false);
    }
    public void PlayerHittedBall(string playerName, List<float> ballsData)
    {
        ballsNames[playerName].AddForce(Quaternion.Euler(0f, ballsData[0], 0f)*Vector3.forward*maxPower*ballsData[1],ForceMode.Impulse);// odpowiednia kolejnosc przeslanych danych
    }
    void OnCollisionEnter(Collision collision)
{
    
    if(collision.collider.tag == "Out of bounds"){

        GameObject collidedObject = collision.gameObject;

        if (collidedObject.transform.IsChildOf(transform))
    {
        // funkjca wysylajaca na serwer collidedObject.name i proszaca o jego lastpostion.x lastposition.y i lastposition.z
        //ballsNames[collidedObject.name].position = new Vector3(x,y,z);
        ballsNames[collidedObject.name].velocity = Vector3.zero;
        ballsNames[collidedObject.name].angularVelocity = Vector3.zero;
    }
    }
   
}
    

}
