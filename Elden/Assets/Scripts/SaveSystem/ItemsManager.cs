using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    private Item[] items;
    private Vector3[] itemsPosition;
    private Quaternion[] itemsRotation;

    public static ItemsManager instance;

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
        items = new Item[transform.childCount];
        itemsPosition = new Vector3[transform.childCount];
        itemsRotation = new Quaternion[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            items[i] = transform.GetChild(i).GetComponent<ItemInteraction>().item;
            itemsPosition[i] = transform.GetChild(i).transform.position;
            itemsRotation[i] = transform.GetChild(i).transform.rotation;
        }
        _saveSystem.worldInfo.items = items;
        _saveSystem.worldInfo.itemsPosition = itemsPosition;
        _saveSystem.worldInfo.itemsRotation = itemsRotation;

    }
    private void Load()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        } 
        for(int i = 0; i < _saveSystem.worldInfo.items.Length; i++ )
        {
            Instantiate(_saveSystem.worldInfo.items[i].prefab, _saveSystem.worldInfo.itemsPosition[i], _saveSystem.worldInfo.itemsRotation[i],transform);
        }
    }

}
