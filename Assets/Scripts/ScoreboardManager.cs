using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{   
    public static ScoreboardManager scoreboardManager;
    public Behaviour canvas;
    public TMP_Text playerName;
    public TMP_Text playerTotalScore;
    public TMP_Text plaeyrScoreHole1;
    public TMP_Text plaeyrScoreHole2;
    public TMP_Text plaeyrScoreHole3;
    public TMP_Text plaeyrScoreHole4;
    public TMP_Text plaeyrScoreHole5;

    void Awake()
    {
        if (scoreboardManager != null)
        {
            Destroy(gameObject);
            return;
        }
        playerName.text = PlayerInfo.playerInfo.playerData[0].playerName.ToString();
        playerTotalScore.text = LevelManager.Instance.totalPutts.ToString();
        plaeyrScoreHole1.text = LevelManager.levelData[0][1].ToString();
        plaeyrScoreHole2.text = LevelManager.levelData[1][1].ToString();
        plaeyrScoreHole3.text = LevelManager.levelData[2][1].ToString();
        plaeyrScoreHole4.text = LevelManager.levelData[3][1].ToString();
        plaeyrScoreHole5.text = LevelManager.levelData[4][1].ToString();
        scoreboardManager = this;
        DontDestroyOnLoad(gameObject);
        canvas.enabled = false;
    }
    public void UptadeScoreboard()
    {
        playerTotalScore.text = LevelManager.Instance.totalPutts.ToString();
        plaeyrScoreHole1.text = LevelManager.levelData[0][1].ToString();
        plaeyrScoreHole2.text = LevelManager.levelData[1][1].ToString();
        plaeyrScoreHole3.text = LevelManager.levelData[2][1].ToString();
        plaeyrScoreHole4.text = LevelManager.levelData[3][1].ToString();
        plaeyrScoreHole5.text = LevelManager.levelData[4][1].ToString();
    }

}


