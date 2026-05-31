using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectsData", menuName = "ScriptableObjects/Subject", order = 1)]

public class Subject : ScriptableObject
{
    public string subjectName;
    public Sprite icon;

    public GameObject prefab;

    public Interaction usable;
}
[Serializable]
public class Interaction
{
    public bool isUsable;
    public float healthAmount;
    public float hungerAmount;
    public float energyAmount;
}
