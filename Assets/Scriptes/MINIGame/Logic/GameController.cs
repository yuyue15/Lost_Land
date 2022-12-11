using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    public UnityEvent OnFinish;

    [Header("游戏数据")]
    public GameH2A_SO gameData;
    public GameH2A_SO[] gameDataArray; 
    public GameObject LineParent;
    public LineRenderer LinePrefab;
    public Ball BallPrefab;
    public Transform[] holderTransform;

    void Start()
    {
        DrawLine();
        CreatBall();
    }
    void OnEnable()
    {
        EventHanderler.CheckGameStateEvent += OnCheackGameStateEvent;
    }
    void OnDisable()
    {
        EventHanderler.CheckGameStateEvent -= OnCheackGameStateEvent;
    }


    private void OnCheackGameStateEvent()
    {
        foreach(var ball in FindObjectsOfType<Ball>()) 
        {
            if((!ball.IsMatch))
            {
                return;
            }
        }
        Debug.Log("游戏结束");
        foreach(var holder in holderTransform)
        {
            holder.GetComponent<Collider2D>().enabled=false;
        }
        EventHanderler.CallGamePassEvent(gameData.gameName);
        OnFinish?.Invoke();
    }
    public void ResetGame()
    {
        for(int i=0;i<LineParent.transform.childCount;i++)
        {
            Destroy(LineParent.transform.GetChild(i).gameObject);

        }
        foreach(var holder in holderTransform)
        {
            if(holder.childCount>0)
            {
                Destroy(holder.GetChild(0).gameObject);
            }
        }
        DrawLine();
        CreatBall();
    }
    public void DrawLine()
    {
        foreach(var connections in gameData.connectionsList) 
        {
            var Line = Instantiate(LinePrefab, LineParent.transform);
            Line.SetPosition(0, holderTransform[connections.form].position);
            Line.SetPosition(1, holderTransform[connections.to].position);
            //创建连接关系
            holderTransform[connections.form].GetComponent<Hoder>().
                LinkHolders.Add(holderTransform[connections.to].GetComponent<Hoder>());
            holderTransform[connections.to].GetComponent<Hoder>().
                LinkHolders.Add(holderTransform[connections.form].GetComponent<Hoder>());

        }
    }
    public void CreatBall()
    {
        for(int i=0;i<gameData.StartBallOrferList.Count;i++) 
        {
            if (gameData.StartBallOrferList[i] == BallName.None)
            {
                holderTransform[i].GetComponent<Hoder>().IsEmpty = true;
                continue;
            }
            Ball ball = Instantiate(BallPrefab, holderTransform[i]);
            holderTransform[i].GetComponent<Hoder>().CheackBall(ball);
            holderTransform[i].GetComponent<Hoder>().IsEmpty=false;
            ball.SetBall(gameData.GetBallDetail(gameData.StartBallOrferList[i]));
        }
    }
    public void SetGameWeekData(int gameWeek)
    {
        gameData = gameDataArray[gameWeek];
        //DrawLine();
        //CreatBall();
    }
}
