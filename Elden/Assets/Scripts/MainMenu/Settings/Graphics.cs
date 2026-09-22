using System;
using UnityEngine;

public class Graphics : MonoBehaviour
{
    [SerializeField] private int[] _graphics;
    [SerializeField] private int[] _shadows;
    [SerializeField] private int[] _lights;
    [SerializeField] private int[] _waters;

    public Settings _settings;

    public void ChangeGraphics(int index)
    {
        _settings.graphicsSettings = _graphics[index];

    }
    public void ChangeShadows(int index)
    {
        _settings.shadowsSettings = _shadows[index];
    }
    public void ChangeLights(int index)
    {
        _settings.lightsSettings = _lights[index];
    }
    public void ChangeWaters(int index)
    {
        _settings.watersSettings = _waters[index];
    }
}
[Serializable]
public class Settings
{
    public int graphicsSettings = 0;
    public int shadowsSettings = 0;
    public int lightsSettings = 0;
    public int watersSettings = 0;

    //public int languageSettings;

    //public int volumSettings;
}
