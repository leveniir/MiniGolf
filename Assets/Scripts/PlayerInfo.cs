using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo playerInfo;
    
    public List<Player> playerData;

    private void Awake()
    {
        if (playerData != null)
        {
            Destroy(gameObject);
            return;
        }
        playerInfo = this;
        DontDestroyOnLoad(gameObject);
        playerData = new List<Player>();
    }

    public void CreatePlayer(string name)
    {
        playerData.Add(new Player(name, 0, 1));
        
    }
    public class Player
    {
        public string playerName;
        public int[] putts;
        public int level;
            public Player(string newName,  int levelCount, int newLevel)
        {
            playerName = newName;
            putts = new int[levelCount];
            level = newLevel;
        }
    }
    
    
}
