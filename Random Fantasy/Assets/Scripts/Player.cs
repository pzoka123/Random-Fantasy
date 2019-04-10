using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player
{
    public static List<string> inventory = new List<string>();
    public static Dictionary<string, int> stats = new Dictionary<string, int>
    {
        { "Strength", 0 },
        { "Defense", 0 },
        { "Knowledge", 0 },
        { "Alchemy", 0 },
        { "Smithing", 0 },
    };
}
