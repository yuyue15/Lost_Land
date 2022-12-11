using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHanderler
{
    public static event Action<ItemDate, int> UpdateUIEvent;

    public static void CallUpdateUIEvent(ItemDate itemDate, int Index)
    {
        UpdateUIEvent?.Invoke(itemDate, Index);
    }
    public static event Action BeforeSceneUnLoadEvent;
    public static void CallBeforeSceneUnLoadEvent()
    {
        BeforeSceneUnLoadEvent?.Invoke();
    }
    public static event Action AfterSceneUnLoadEvent;
    public static void CallAfterSceneUnLoadEvent()
    {
        AfterSceneUnLoadEvent?.Invoke();
    }
    public static event Action<ItemDate, bool> ItemSelectedEvent;
    public static void CallItemSelectedEvent(ItemDate itemDate, bool IsSelected)
    {
        ItemSelectedEvent?.Invoke(itemDate, IsSelected);
    }
    public static event Action<ItemName> ItemNameUseEvent;
    public static void CallItemNameEvent(ItemName itemName)
    {
        ItemNameUseEvent?.Invoke(itemName);
    }
    public static event Action<int> ChangeItemEvent;
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }
    public static event Action<string> ShowDialogueTextEvent;
    public static void CallShowDialogueTextEvent(string text)
    {
        ShowDialogueTextEvent?.Invoke(text);
    }
    public static event Action<GameState> GameStateChangedEvent;
    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangedEvent?.Invoke(gameState);
    }
    public static event Action CheckGameStateEvent;
    public static void CallCheckGameStateEvent()
    {
        CheckGameStateEvent?.Invoke();
    }
    public static event Action<string> GamePassEvent;
    public static void CallGamePassEvent(string gameName)
    {
        GamePassEvent?.Invoke(gameName);
    }
    public static event Action<int> StartNewGameEvent;
    public static void CallStartNewGameEvent(int gameWeek)
    {
        StartNewGameEvent?.Invoke(gameWeek);
    }

}
