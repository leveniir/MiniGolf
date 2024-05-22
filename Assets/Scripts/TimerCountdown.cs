using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;

public class TimerCountdown : MonoBehaviour {
    public TextMeshProUGUI timerText; 
    public static float countdownTime = 90f; 

    void Start() {
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer() {
        while (countdownTime > 0) { 
            int minutes = (int)(countdownTime / 60); 
            int seconds = (int)(countdownTime % 60); 
            timerText.text = $"{minutes:00}:{seconds:00}"; 

            yield return new WaitForSeconds(1); 
            countdownTime -= 1; 
        }

        timerText.text = "00:00";
        countdownTime = 0;
    }

}
