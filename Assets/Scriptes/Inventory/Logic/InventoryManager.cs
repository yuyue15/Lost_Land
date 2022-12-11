using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>,ISaveavle
{
    public ItemDateList ItemDate;
    [SerializeField] private List<ItemName> itemsList = new List<ItemName>();
    void OnEnable()
    {
        EventHanderler.ItemNameUseEvent += OnItemNameUseEvent;
        EventHanderler.ChangeItemEvent += OnChangeItemEvent;
        EventHanderler.AfterSceneUnLoadEvent += OnAfterSceneUnLoadEvent;
        EventHanderler.StartNewGameEvent += OnStartNewGameEvent;

    }
    void OnDisable()
    {
        EventHanderler.ItemNameUseEvent -= OnItemNameUseEvent;
        EventHanderler.ChangeItemEvent -= OnChangeItemEvent;
        EventHanderler.AfterSceneUnLoadEvent -= OnAfterSceneUnLoadEvent;
        EventHanderler.StartNewGameEvent -= OnStartNewGameEvent;
    }
    void Start()
    {
        ISaveavle saveavle = this;
        saveavle.SaveableRegister();
    }
    private void OnStartNewGameEvent(int gameWeek)
    {
        itemsList.Clear();
    }

    private void OnAfterSceneUnLoadEvent()
    {
        if(itemsList.Count==0)
        {
            EventHanderler.CallUpdateUIEvent(null, -1);
        }
        else
        {
            for(int i=0;i<itemsList.Count;i++) 
            {
                EventHanderler.CallUpdateUIEvent(ItemDate.GetItemDate(itemsList[i]), i);
            }
        }
    }

    private void OnChangeItemEvent(int index)
    {
        if(index>=0&&index<itemsList.Count) 
        {
            ItemDate item = ItemDate.GetItemDate(itemsList[index]);
            EventHanderler.CallUpdateUIEvent(item, index);
        }
    }

    private void OnItemNameUseEvent(ItemName itemName)
    {
        var index = GetItem(itemName);
        itemsList.RemoveAt(index);
        if(itemsList.Count==0)
        {
            EventHanderler.CallUpdateUIEvent(null, -1);
        }
    }

    public void AddItem(ItemName itemName)
    {
        if(!itemsList.Contains(itemName))
        {
            itemsList.Add(itemName);
            //:ui显示
            EventHanderler.CallUpdateUIEvent(ItemDate.GetItemDate(itemName), itemsList.Count - 1);
        }
    }
    public int GetItem(ItemName itemName)
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            if (itemsList[i] == itemName) 
            {
                return i;
            }
        }
        return -1;
    }

    public GameSaveData GenerateSaveDara()
    {
        GameSaveData gameSaveData = new GameSaveData();
        gameSaveData.itemsList= this.itemsList;
        return gameSaveData;
    }

    public void RestorGameData(GameSaveData data)
    {
        this.itemsList=data.itemsList;
    }
}
