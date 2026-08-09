using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private Subject[] build;
    private Vector3[] buildingsPosition;
    private Quaternion[] buildingsRotation;

    public static BuildingManager instance;

    private SaveSystem _saveSystem;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _saveSystem = SaveSystem.instance;

        _saveSystem.OnSaveRequested += Save;
        _saveSystem.OnLoadRequested += Load;
    }

    private void OnDisable()
    {
        _saveSystem.OnSaveRequested -= Save;
        _saveSystem.OnLoadRequested -= Load;
    }
    private void Save()
    {
        build = new Subject[transform.childCount];
        buildingsPosition = new Vector3[transform.childCount];
        buildingsRotation = new Quaternion[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            build[i] = transform.GetChild(i).GetComponent<SubjectInteraction>().subject;
            buildingsPosition[i] = transform.GetChild(i).transform.position;
            buildingsRotation[i] = transform.GetChild(i).transform.rotation;
        }
        _saveSystem.worldInfo.build = build;
        _saveSystem.worldInfo.buildingsPosition = buildingsPosition;
        _saveSystem.worldInfo.buildingsRotation = buildingsRotation;

    }
    private void Load()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < _saveSystem.worldInfo.build.Length; i++)
        {
            Instantiate(_saveSystem.worldInfo.build[i].prefab, _saveSystem.worldInfo.buildingsPosition[i], _saveSystem.worldInfo.buildingsRotation[i], transform);
        }
    }
}
