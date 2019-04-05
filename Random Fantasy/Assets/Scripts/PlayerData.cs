using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<ItemData> inventory;
    public List<StatsData> stats;

    public PlayerData()
    {
        inventory = Player.inventory;
        stats = Player.stats;
    }
}
