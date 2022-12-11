using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>,ISaveavle
{
    public string StartScene;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;

    private bool isFade;
    private bool CanTriansition;
    void Start()
    {
        ISaveavle saveavle = this;
        saveavle.SaveableRegister();
    }
    void OnEnable()
    {
        EventHanderler.GameStateChangedEvent += OnGameStateChangeEvent;
        EventHanderler.StartNewGameEvent += OnStartNewGameEvent;
    }
    void OnDisable()
    {
        EventHanderler.GameStateChangedEvent -= OnGameStateChangeEvent;
        EventHanderler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        StartCoroutine(TransitionToScence("Menu", StartScene));
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        CanTriansition = gameState == GameState.GamePlayer;
    }

    public void Transition(string form,string to)
    {
        if(!isFade&&CanTriansition)
        {
            StartCoroutine(TransitionToScence(form, to));
        }
        
    }
    /// <summary>
    /// 创建协程
    /// </summary>
    /// <param name="form"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private IEnumerator TransitionToScence(string form,string to)
    {
        
        yield return Fade(1);
        
        if (form!=string.Empty) 
        {
            EventHanderler.CallBeforeSceneUnLoadEvent();
            yield return SceneManager.UnloadSceneAsync(form);

        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        //设置新场景为激活场景
        Scene newScence = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScence);
        EventHanderler.CallAfterSceneUnLoadEvent();
        yield return Fade(0);
    }
    /// <summary>
    /// 设置场景渐变协程
    /// </summary>
    /// <param name="TargetAlpha">1黑，0透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float TargetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - TargetAlpha) / fadeDuration;
        while(!Mathf.Approximately(fadeCanvasGroup.alpha,TargetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, TargetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }

    public GameSaveData GenerateSaveDara()
    {
        GameSaveData saveData= new GameSaveData();
        saveData.currentScene=SceneManager.GetActiveScene().name;
        return saveData;
    }

    public void RestorGameData(GameSaveData data)
    {
        Transition("Menu", data.currentScene);
    }
}
