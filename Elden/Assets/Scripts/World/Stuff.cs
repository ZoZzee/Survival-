using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StuffData", menuName = "ScriptableObjects/Stuff", order = 1)]
public class Stuff : ScriptableObject
{
    public string stuffName;
    public Sprite icon;

    public GameObject prefab;

    public Usable usable;
}