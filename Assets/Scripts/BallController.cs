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
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
        line = GetComponent<LineRenderer>();
        Cursor.visible = false;
        maxPower = 10;
    
        showLevel.text = (LevelManager.Instance.currentLevel-1).ToString();
        maxPutts = 2;

    }

    void Update()
    {
       if(ball.velocity.magnitude == 0f){
        Debug.Log(Vector3.Distance(transform.position, lastPosition).ToString());
        if((ball.velocity.magnitude==0 && putts == maxPutts && Vector3.Distance(transform.position, lastPosition) > 0f) || TimerCountdown.countdownTime == 0){
            LevelManager.Instance.CountPutts(maxPutts+3);
            LevelManager.Instance.CountTotalPutts();
            LevelManager.Instance.currentLevel++;
            ScoreboardManager.scoreboardManager.UptadeScoreboard();
            TimerCountdown.countdownTime = 90;
            SceneManager.LoadScene(LevelManager.Instance.currentLevel);
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            Putt();
            UpdateLinePositions();
        }
        if(Input.GetKey(KeyCode.Space)){
            PowerUp();
            UpdateLinePositions();
        }
       } else {
        line.enabled = false;
       }
       if(Input.GetKey(KeyCode.Tab))
       {
            ScoreboardManager.scoreboardManager.canvas.enabled=true;
       } else {
            ScoreboardManager.scoreboardManager.canvas.enabled=false;
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
        power = 0;
        powerSlider.value = 0;
        powerUpTime = 0;
        putts++;
        LevelManager.Instance.CountPutts(putts);
        LevelManager.Instance.CountTotalPutts();
        ScoreboardManager.scoreboardManager.UptadeScoreboard();
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
            holeTime = 0;
            LevelManager.Instance.CountPutts(putts);
            LevelManager.Instance.currentLevel++;
            LevelManager.Instance.CountTotalPutts();
            SceneManager.LoadScene(LevelManager.Instance.currentLevel);
        }
    }

    private void LeftHole(){
            holeTime = 0;
        
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.collider.tag == "Out of bounds"){
            if(putts == maxPutts)
            {
                LevelManager.Instance.CountPutts(maxPutts+3);
            LevelManager.Instance.CountTotalPutts();
            LevelManager.Instance.currentLevel++;
            ScoreboardManager.scoreboardManager.UptadeScoreboard();
            TimerCountdown.countdownTime = 30;
            SceneManager.LoadScene(LevelManager.Instance.currentLevel);
            }
            transform.position = lastPosition;
            ball.velocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
        }
    }
}
