using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectsData", menuName = "ScriptableObjects/Subject", order = 1)]

public class Subject : ScriptableObject
{
    public string subjectName;

    public GameObject prefab;

    public Sleap sleap;
}
[Serializable]
public class Sleap
{
    public bool isUsable;
    public float healthAmount;
    public float hungerAmount;
    public float energyAmount;
    public float sleepAmount;
}
