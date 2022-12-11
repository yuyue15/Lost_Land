using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameH2A_SO",menuName ="面板/小游戏数据")]

public class GameH2A_SO : ScriptableObject
{
    public string gameName;
    [Header("球的名字和对应图片")]
    public List<BallDetail> ballDetailsList;
    [Header("游戏逻辑数据")]
    public List<Connections> connectionsList;
    public List<BallName> StartBallOrferList;
    public BallDetail GetBallDetail(BallName ballName)
    {
        return ballDetailsList.Find(b=>b.ballName == ballName);
    }
}
[Serializable]
public class BallDetail
{
    public BallName ballName;
    public Sprite wrongSprite;
    public Sprite rightSprite;
}
[Serializable]
public class Connections
{
    public int form;
    public int to;
}