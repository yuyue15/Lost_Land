using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : Singleton<SaveLoadManager> 
{
    private string jsonFolder;
    private List<ISaveavle> saveavleList=new List<ISaveavle>(); 
    private Dictionary<string,GameSaveData> saveDataDic= new Dictionary<string,GameSaveData>();

    protected override void Awake()
    {
        base.Awake();
        jsonFolder = Application.persistentDataPath + "/SAVE/";
        Debug.Log(jsonFolder);

    }
    void OnEnable()
    {
        EventHanderler.StartNewGameEvent += OnStartNewGameEvent;
    }
    void OnDisable()
    {
        EventHanderler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        var resultPath= jsonFolder + "data.sav";
        if(File.Exists(resultPath)) 
        {
            File.Delete(resultPath);
        }
    }

    public void Register(ISaveavle saveavle)
    {
        saveavleList.Add(saveavle);
    }
    public void Save()
    {
        saveDataDic.Clear();

        foreach(var saveavle in saveavleList) 
        {
            saveDataDic.Add(saveavle.GetType().Name,saveavle.GenerateSaveDara());
        }
        var resultPath = jsonFolder + "data.sav" ;
        var jsonData=JsonConvert.SerializeObject(saveDataDic,Formatting.Indented);
        if (!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);

        }
        File.WriteAllText(resultPath, jsonData);
    }
    public void Load()
    {
        var resultPath = jsonFolder+ "data.sav" ;
        if (!File.Exists(resultPath))
        {
            return;
        }
        var stringData=File.ReadAllText(resultPath);
        var jsonData=JsonConvert.DeserializeObject<Dictionary<string,GameSaveData>>(stringData);
        foreach(var saveable in saveavleList)
        {
            saveable.RestorGameData(jsonData[saveable.GetType().Name]);
        }
    }
}
