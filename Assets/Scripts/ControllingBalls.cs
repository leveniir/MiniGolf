using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllingBalls : MonoBehaviour
{
    public List<Rigidbody> balls;
    public int numOfPlayers;
    public int maxPower = 10;
    public List<List<float>> ballsData;
    void Start()
    {
        // numofPlayers = funkcja() zwracanie ilosci graczy z serwera 
        for (int i = 0; i< numOfPlayers;i++)
        {
            balls[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // funkcja pobierajaca prdekosci kulerk z serwera
        for (int i = 0; i< numOfPlayers;i++)
        {
            if (ballsData[i][0] != 0 && ballsData[i][1]!= 0)
            balls[i].AddForce(Quaternion.Euler(0f, ballsData[i][0], 0f)*Vector3.forward*maxPower*ballsData[i][1],ForceMode.Impulse);
            ballsData[i][0] = 0;
            ballsData[i][1] = 0;
            
        }
    }

}
