using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float maxPower;
    public Slider powerSlider;
    public Transform cam;
    public TextMeshProUGUI puttsCountLabel;
    public TextMeshProUGUI showLevel;
    public float minHoleTime;
    public int ballStop;
    static public bool ballInHole;
    static public bool allReady; // zwrocenie czy wszycy gracze gotowi 


    private LineRenderer line;
    private Rigidbody ball;
    private float powerUpTime;
    private float power;
    private int putts;
    private float holeTime;
    private Vector3 lastPosition;
    private int maxPutts;
   


    void Awake()
    {

        ballInHole = false;
        ballStop = 0;

        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
        line = GetComponent<LineRenderer>();
        Cursor.visible = false;
        maxPower = 10;
    
        showLevel.text = (LevelManager.Instance.currentLevel-1).ToString();
        maxPutts = 15;

    }

    void Update()
    {   
        print(ball.velocity.magnitude.ToString());
       if(ball.velocity.magnitude < 0.1f){
        if((ball.velocity.magnitude==0 && putts == maxPutts && Vector3.Distance(transform.position, lastPosition) > 0f) || TimerCountdown.countdownTime == 0){
            if (ballStop == 0)
            {
                // wywolanie funkcji na serwer, ze pilka sie zatrzymal
                ballStop++;
            }
            // nizej wywloanie funkcji do pobrania ze grcz jest gotow przejsc dalej
            // allRead = funkcja();
            if (allReady){
            allReady = false;
            ballInHole = false;
            LevelManager.Instance.CountPutts(maxPutts+3);
            LevelManager.Instance.CountTotalPutts();
            LevelManager.Instance.currentLevel++;
            //ScoreboardManager.scoreboardManager.UptadeScoreboard();
            TimerCountdown.countdownTime = 90f;
            SceneManager.LoadScene(LevelManager.Instance.currentLevel);
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) && putts != maxPutts){
            // wywolanie funkcji przesylajacej na serwer, ze pilka zostala uderzona
            ballStop = 0;
            Putt();
            UpdateLinePositions();
        }
        if(Input.GetKey(KeyCode.Space) && putts != maxPutts){
            PowerUp();
            UpdateLinePositions();
        }
       } else {
        line.enabled = false;
       }
       
    }

    private void UpdateLinePositions()
    {
        if(holeTime == 0){line.enabled = true;}
        line.SetPosition(0,transform.position);
        line.SetPosition(1,transform.position+Quaternion.Euler(0f, cam.eulerAngles.y, 0f)*Vector3.forward*power);
    }

    private void Putt() {
        lastPosition = transform.position;
        ball.AddForce(Quaternion.Euler(0f, cam.eulerAngles.y, 0f)*Vector3.forward*maxPower*power,ForceMode.Impulse);
        // funkcja wysylajaca na serwer cam.eulerAngles.y, power i lastPosition.x, last.position.y, lastposition.y
        power = 0;
        powerSlider.value = 0;
        powerUpTime = 0;
        putts++;
        LevelManager.Instance.CountPutts(putts);
        LevelManager.Instance.CountTotalPutts();
        //ScoreboardManager.scoreboardManager.UptadeScoreboard();
        puttsCountLabel.text = putts.ToString();
    }

    private void PowerUp()
    {
        powerUpTime += Time.deltaTime;
        power = Mathf.PingPong(powerUpTime,1);
        powerSlider.value = power;
    }
 
    private void OnTriggerStay(Collider other){
        if(other.tag == "Hole")
            {
                CountHoleTime();
            }
    }
    private void OnTriggerExit(Collider other){
        if(other.tag == "Hole"){
            LeftHole();
            }
    }
    private void CountHoleTime(){
        holeTime += Time.deltaTime;
        if(holeTime >= minHoleTime){
            ballInHole = true; // nizej wywloanie funkcji do pobrania ze grcz jest gotow przejsc dalej
            // allRead = funkcja();
            if (allReady){
            allReady = false;
            holeTime = 0;
            ballInHole = false;
            LevelManager.Instance.CountPutts(putts);
            LevelManager.Instance.currentLevel++;
            LevelManager.Instance.CountTotalPutts();
            TimerCountdown.countdownTime = 90f;
            SceneManager.LoadScene(LevelManager.Instance.currentLevel);
            }
        }
    }

    private void LeftHole(){
            holeTime = 0;
        
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.collider.tag == "Out of bounds"){
            if(putts == maxPutts)
            {
                // nizej wywloanie funkcji do pobrania ze grcz jest gotow przejsc dalej
                // allRead = funkcja();
                if (allReady)
                {
                    LevelManager.Instance.CountPutts(maxPutts+3);
                    LevelManager.Instance.CountTotalPutts();
                    LevelManager.Instance.currentLevel++;
                    //ScoreboardManager.scoreboardManager.UptadeScoreboard();
                    TimerCountdown.countdownTime = 90f;
                    SceneManager.LoadScene(LevelManager.Instance.currentLevel);
                }
          
            }
            transform.position = lastPosition;
            ball.velocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
        }
    }
}
