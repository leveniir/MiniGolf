using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
   public void RestartGame()
   {
        LevelManager.Instance.totalPutts = 0;
        LevelManager.Instance.currentLevel = 3;
        ResetLevelData(5);
        //ScoreboardManager.scoreboardManager.UptadeScoreboard();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);

   }
   public static void ResetLevelData(int numberOfLevels)
{
    LevelManager.levelData = new List<List<int>>();

    for (int i = 1; i <= numberOfLevels; i++) 
    {
        List<int> innerList = new List<int>();

        innerList.Add(i); 
        innerList.Add(0);
        
        LevelManager.levelData.Add(innerList);
    }
}
}
