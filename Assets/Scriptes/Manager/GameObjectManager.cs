using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameObjectManager : MonoBehaviour,ISaveavle
{
    private Dictionary<ItemName, bool> ItemAvailableDic = new Dictionary<ItemName, bool>();
    private Dictionary<string,bool> interactiveDic=new Dictionary<string,bool>();
    void OnEnable()
    {
        EventHanderler.BeforeSceneUnLoadEvent += OnBeforeSceneUnLoadEvent;
        EventHanderler.AfterSceneUnLoadEvent += OnAfterSceneUnLoadEvent;
        EventHanderler.UpdateUIEvent += OnUpdateUIEvent;
        EventHanderler.StartNewGameEvent += OnStartNewGameEvent;
    }

    void OnDisable()
    {
        EventHanderler.BeforeSceneUnLoadEvent -= OnBeforeSceneUnLoadEvent;
        EventHanderler.AfterSceneUnLoadEvent -= OnAfterSceneUnLoadEvent;
        EventHanderler.UpdateUIEvent -= OnUpdateUIEvent;
        EventHanderler.StartNewGameEvent -= OnStartNewGameEvent;
    }
    void Start()
    {
        ISaveavle saveavle = this;
        saveavle.SaveableRegister();
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        ItemAvailableDic.Clear();
        interactiveDic.Clear();
    }

    /// <summary>
    /// 卸载场景之前
    /// </summary>
    private void OnAfterSceneUnLoadEvent()
    {

        foreach (var item in FindObjectsOfType<Item>())
        {
            //是否有在字典中，否则不显示
            if (!ItemAvailableDic.ContainsKey(item.ItemName))
            {
                ItemAvailableDic.Add(item.ItemName, true);
            }
            else
            {
                item.gameObject.SetActive(ItemAvailableDic[item.ItemName]);
            }
        }
        foreach (var item in FindObjectsOfType<InterActive>())
        {
            if (interactiveDic.ContainsKey(item.name))
            {
                item.IsDone = interactiveDic[item.name];

            }
            else
            {
                interactiveDic.Add(item.name, item.IsDone);
            }
        }
    }
    private void OnBeforeSceneUnLoadEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!ItemAvailableDic.ContainsKey(item.ItemName))
            {
                ItemAvailableDic.Add(item.ItemName, true);
            }
        }
        foreach(var item in FindObjectsOfType<InterActive>())
        {
            if(interactiveDic.ContainsKey(item.name))
            {
                interactiveDic[item.name] = item.IsDone;

            }
            else
            {
                interactiveDic.Add(item.name, item.IsDone);
            }
        }
    }
    private void OnUpdateUIEvent(ItemDate item,int arg2)
    {
        if (item != null)
        {
            ItemAvailableDic[item.ItemName] = false;
        }
    }

    public GameSaveData GenerateSaveDara()
    {
        GameSaveData gameSaveData = new GameSaveData();
        gameSaveData.ItemAvailableDic = this.ItemAvailableDic;
        gameSaveData.interactiveDic = this.interactiveDic;
        return gameSaveData;
    }

    public void RestorGameData(GameSaveData data)
    {
        this.ItemAvailableDic = data.ItemAvailableDic;
        this.interactiveDic= data.interactiveDic;
    }
}
