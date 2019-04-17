using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatData : MonoBehaviour
{
    public string victory;
    public string defeat;
    public List<string> items;
    public List<StatsData> stats;
}
