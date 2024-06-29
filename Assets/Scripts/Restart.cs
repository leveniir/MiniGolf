using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        LevelManager.Instance.currentLevel = 3;
        ResetLevelData(5);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        TimerCountdown.countdownTime = 90f;
    }
    public static void ResetLevelData(int numberOfLevels)
    {
        LevelManager.Instance.levelData = new int[6];
    }
}
