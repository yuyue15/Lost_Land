using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,ISaveavle
{
    private Dictionary<string,bool> MiniGameStateDic=new Dictionary<string,bool>();
    private GameController currentGame;
    private int gameWeek;

    void OnEnable()
    {
        EventHanderler.AfterSceneUnLoadEvent += OnAfterSceneUnLoadEvent;
        EventHanderler.GamePassEvent += OnGamePassEvent;
        EventHanderler.StartNewGameEvent += OnStartNewGameEvent;
    }
    void OnDisable()
    {
        EventHanderler.AfterSceneUnLoadEvent -= OnAfterSceneUnLoadEvent;
        EventHanderler.GamePassEvent -= OnGamePassEvent;
        EventHanderler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        this.gameWeek = gameWeek;
        MiniGameStateDic.Clear();
    }

    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        EventHanderler.CallGameStateChangeEvent(GameState.GamePlayer);
        ISaveavle saveavle = this;
        saveavle.SaveableRegister();
    }
    private void OnAfterSceneUnLoadEvent()
    {
        foreach(var miniGame in FindObjectsOfType<MiniGame>())
        {
            if(MiniGameStateDic.TryGetValue(miniGame.gameName,out bool IsPass))
            {
                miniGame.IsPass= IsPass;
                miniGame.UpDateMiniGameState();
            }
        }
        currentGame=FindObjectOfType<GameController>();
        currentGame?.SetGameWeekData(gameWeek);
    }
    private void OnGamePassEvent(string gameName)
    {
        MiniGameStateDic[gameName] = true;
    }

    public GameSaveData GenerateSaveDara()
    {
        GameSaveData gameSaveData=new GameSaveData();
        gameSaveData.gameWeek = this.gameWeek;
        gameSaveData.MiniGameStateDic = this.MiniGameStateDic;
        return gameSaveData;
    }

    public void RestorGameData(GameSaveData data)
    {
        this.gameWeek = data.gameWeek;
        this.MiniGameStateDic=data.MiniGameStateDic;
    }
}
