using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveData
{
    public int gameWeek;
    public string currentScene;
    public Dictionary<string, bool> MiniGameStateDic;
    public Dictionary<ItemName, bool> ItemAvailableDic;
    public Dictionary<string, bool> interactiveDic;
    public List<ItemName> itemsList;
}
