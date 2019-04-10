using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<string> inventory;
    public Dictionary<string, int> stats;

    public PlayerData()
    {
        inventory = Player.inventory;
        stats = Player.stats;
    }
}
