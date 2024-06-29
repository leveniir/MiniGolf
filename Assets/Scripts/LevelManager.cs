using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int[] levelData;//Numer dolka, ilosc strzalow
    public int currentLevel;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        levelData = new int[6];
        currentLevel = 3;
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void CountPutts(int putts)
    {
        levelData[currentLevel - 3] = putts;
    }
    public void ChangeLevel()
    {
        currentLevel++;
    }
    public void CountTotalPutts()
    {
        int counter = 0;
        for (int i = 0; i < 5; i++)
        {
            counter += levelData[i];
        }
        levelData[5] = counter;
    }
}
