using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public WorldInfo worldInfo;

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
        Save("worldInfo",worldInfo);

        Debug.Log("Game saved");
    }
    public void LoadAll()
    {
        playerInfo = Load<PlayerInfo>("playerInfo");
        worldInfo = Load<WorldInfo>("worldInfo");
        
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
    public float health;
    public float hunger;
    public float energy;
    public float sleep;

    public Vector3 position;

    public Item[] items;
    public int[] counts;
}

[Serializable]

public class WorldInfo
{
    public Item[] items;
    public Vector3[] itemsPosition;
    public Quaternion[] itemsRotation;
    public Subject[] build;
    public Vector3[] buildingsPosition;
    public Quaternion[] buildingsRotation;

}