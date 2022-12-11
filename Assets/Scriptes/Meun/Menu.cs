using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ContinueGame()
    {
        //TODO 加载游戏进度
        SaveLoadManager.Instance.Load();
    }
    public void GoBackToMenu()
    {
        var currentScene=SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transition(currentScene, "Menu");
        //TODO 保存游戏进度
        SaveLoadManager.Instance.Save();
    }
    public void StartGameWeek(int gameWeek)
    {
        EventHanderler.CallStartNewGameEvent(gameWeek);
    }
}
