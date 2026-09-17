using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class inventoryController : MonoBehaviour
{
    [Header("Selection")]
    [SerializeField] private int currentSelection;


    [Header("Input")]
    [SerializeField] private InputActionReference _leftSelectionAction;
    [SerializeField] private InputActionReference _rightSelectionAction;
    [SerializeField] private InputActionReference _scrollSelectionAction;
    [SerializeField] private InputActionReference _dropAction;
    [SerializeField] private InputActionReference _useAction;


    [Header("Tools")]

    public Animator handAnimator;
    public Item currentTool;
    [SerializeField] private Transform _hand;
    private GameObject _currentToolInHand;

    private Item _currentBuild;

    [Header("References")]
    private NeedsManager _needsManager;
    [SerializeField] public Inventory _playerInventory; // Зробив публічним для бистрого прототипування інвентарю
    [SerializeField] private PlayerBuilding _playerBuilding;
    public Cell[] cells;
    private Camera _mainCamera;

    private SaveSystem _saveSystem;


    private void Start()
    {
        cells = _playerInventory.inventory.cells;
        _needsManager = NeedsManager.instance;
        _mainCamera = Camera.main;
        RefreshSelection();

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
        _saveSystem.playerInfo.items = _playerInventory.inventory.items;
        _saveSystem.playerInfo.counts = _playerInventory.inventory.counts;

    }
    private void Load()
    {
        _playerInventory.inventory.items = _saveSystem.playerInfo.items;
        _playerInventory.inventory.counts = _saveSystem.playerInfo.counts;

        RefreshSelection();
        _playerInventory.Refresh();
    }

    private void Update()
    {
        HandleSelection();
        HandleDrop();
        HandleUse();
    }

    private void HandleUse()
    {
        if (_useAction.action.triggered)
        {
            if (_playerInventory.inventory.items[currentSelection] && _playerInventory.inventory.items[currentSelection].usable.isUsable)
            {
                _needsManager.UseItem(_playerInventory.inventory.items[currentSelection]);
                _playerInventory.inventory.counts[currentSelection]--;
                _playerInventory.Refresh();
            }
        }
    }

    private void HandleDrop()
    {
        if (_dropAction.action.triggered && _playerInventory.inventory.items[currentSelection])
        {
            Instantiate(_playerInventory.inventory.items[currentSelection].prefab, _mainCamera.transform.position + _mainCamera.transform.forward, Quaternion.identity,ItemsManager.instance.transform);
            _playerInventory.ItemDropped(_playerInventory.inventory,currentSelection);
            RefreshSelection();
        }
    }
    private void HandleSelection()
    {
        if (_leftSelectionAction.action.triggered)
        {
            SetSelection(-1);
        }
        else if (_rightSelectionAction.action.triggered)
        {
            SetSelection(1);
        }

        float scroll = _scrollSelectionAction.action.ReadValue<float>();

        if (scroll < 0)
        {
            SetSelection(-1);
        }
        else if (scroll > 0)
        {
            SetSelection(1);
        }
    }

    private void SetSelection(int value)
    {
        currentSelection += value;
        if (currentSelection < 0)
        {
            currentSelection = _playerInventory.inventory.cells.Length - 1;  //Перехід на останню ячейку інвентарря
        }

        else if (currentSelection > _playerInventory.inventory.cells.Length - 1)
        {
            currentSelection = 0;  //Перехід на першу ячейку інвентарря
        }
        RefreshSelection();
    }

    public void MunisCurrentSelection()
    {
        _playerInventory.inventory.counts[currentSelection]--;
        _playerInventory.Refresh();
        RefreshSelection();
    }

    public void RefreshSelection()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].selection.SetActive(false);
        }

        cells[currentSelection].selection.SetActive(true);
        RefreshTool();
        RefreshBuild();
    }
    private void RefreshBuild()
    {
        if (_playerInventory.inventory.items[currentSelection] && _playerInventory.inventory.items[currentSelection].build.isBuild)
        {
            if (!_currentBuild)
            {
                _currentBuild = _playerInventory.inventory.items[currentSelection];
                _playerBuilding.NewBuild(_currentBuild.build.prefab);
            }
            else
            {
                _playerBuilding.ExidBuildMode();
                _currentBuild = _playerInventory.inventory.items[currentSelection];
                _playerBuilding.NewBuild(_currentBuild.build.prefab);
            }
        }
        else
        {
            _playerBuilding.ExidBuildMode();
            _currentBuild = null;
        }
    }
    private void RefreshTool()
    {
        if (_playerInventory.inventory.items[currentSelection] && _playerInventory.inventory.items[currentSelection].tool.isTool)
        {
            if (currentTool && currentTool != _playerInventory.inventory.items[currentSelection])
            {
                _currentToolInHand.SetActive(false);
                _currentToolInHand = null;
                currentTool = null;
            }
            for (int i = 0; i < _hand.childCount; i++)
            {
                if (_hand.GetChild(i).name == _playerInventory.inventory.items[currentSelection].itemName)
                {
                    _hand.GetChild(i).gameObject.SetActive(true);
                    currentTool = _playerInventory.inventory.items[currentSelection];
                    _currentToolInHand = _hand.GetChild(i).gameObject;
                    handAnimator.Play("TakeTool");
                }
            }
        }
        else
        {
            for (int i = 0; i < _hand.childCount; i++)
            {
                if (_currentToolInHand)
                {
                    StartCoroutine(Disactivate(_currentToolInHand));
                    handAnimator.Play("HideTool");
                }
                //_hand.GetChild(i).gameObject.SetActive(false);
                currentTool = null;
                _currentToolInHand = null;
            }
        }
    }

    private IEnumerator Disactivate(GameObject obgectToDisactivate)
    {
        yield return new WaitForSeconds(0.25f);
        obgectToDisactivate.SetActive(false);
    }
}
