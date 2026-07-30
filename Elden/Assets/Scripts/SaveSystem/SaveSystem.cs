using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public PlayerInfo playerInfo;

    public event Action OnSaveRequested;
    public event Action OnLoadRequested;

    public static SaveSystem instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            SaveAll();
        }
        if(Input.GetKeyDown(KeyCode.F6))
        {
            LoadAll();
        }
    }

    public void SaveAll()
    {
        OnSaveRequested?.Invoke();

        Save("playerInfo",playerInfo);
        Debug.Log("Game saved");
    }
    public void LoadAll()
    {
        playerInfo = Load<PlayerInfo>("playerInfo");
        
        OnLoadRequested?.Invoke();
        
        Debug.Log("Game loaded");

    }

    private void Save<T>(string fileName, T data)
    {
        string fullPath = Application.persistentDataPath + $"/{fileName}.json";
        string json = JsonUtility.ToJson(data,true);
        File.WriteAllText(fullPath, json);

    }
    private T Load<T>(string fileName)
    {
        string fullPath = Application.persistentDataPath + $"/{fileName}.json";

        if(File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            return JsonUtility.FromJson<T>(json);
        }

        return default;
    }
}
[Serializable]
public class PlayerInfo
{
    public Vector3 position;

    public Item[] items;
    public int[] counts;
}