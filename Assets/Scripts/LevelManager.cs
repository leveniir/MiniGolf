using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public static List<List<int>> levelData;//Numer dolka, ilosc strzalow
    public int totalPutts;
    public int currentLevel;

    private void Awake() {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        currentLevel = 3;
        totalPutts = 0;
        Instance = this;
        DontDestroyOnLoad(gameObject);

        levelData = new List<List<int>>();

        for (int i = 1; i <= 5; i++) 
        {
            List<int> innerList = new List<int>();

            innerList.Add(i); 
            innerList.Add(0); 
            levelData.Add(innerList);
        }

    }
    public void CountPutts(int putts)
    {
        levelData[currentLevel-3][1] = putts;
    }
    public void ChangeLevel()
    {
        currentLevel++;
    }
    public void CountTotalPutts()
    {
        int counter = 0;
        foreach (var list in levelData) 
        {
            counter += list[1]; 
        }
        totalPutts = counter;
    }
}
