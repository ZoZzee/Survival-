using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    public PlayerInfo playerInfo;

    private static SaveSystem instance;

    private void Awake()
    {
        instance = this;
    }
}
[Serializable]
public class PlayerInfo
{
    public Vector3 position;
}