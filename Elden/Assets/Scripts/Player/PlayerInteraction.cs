using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction parameters")]
    [SerializeField] private float _interactionDistance;

    [Header("Tools")]
    [SerializeField] private float _toolsUseDistance;

    [Header(" UI")]
    [SerializeField] private TMP_Text _interactionText;

    [Header("Referenses")]
    [SerializeField] private NeedsManager _needManager;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private inventoryController _inventoryController;

    [Header("Input")]
    [SerializeField] private InputActionReference _interactAction;
    [SerializeField] private InputActionReference _useToolAction;

    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            if (hit.collider.gameObject.TryGetComponent<ItemInteraction>(out ItemInteraction itemToInteract))
            {
                _interactionText.text = itemToInteract.item.itemName;

                if (_interactAction.action.triggered)
                {
                    _playerInventory.AddItem(itemToInteract.item);
                    _inventoryController.RefreshTool();
                    Destroy(hit.collider.gameObject);
                }
            }
            if (hit.collider.gameObject.TryGetComponent<StuffInteraction>(out StuffInteraction stuffInteraction))
            {
                _interactionText.text = stuffInteraction.stuff.stuffName;
                if (_interactAction.action.triggered)
                {
                    _needManager.Sleep.Restore(_needManager.sleepMax);
                }
            }
        }
        else
        {
            _interactionText.text = "";
        }
        if (_useToolAction.action.triggered && _inventoryController.currentTool)
        {
            _inventoryController.handAnimator.Play("Use");

            Ray rayTool = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hitTool;
            if (Physics.Raycast(rayTool, out hitTool, _toolsUseDistance))
            {
                if(hitTool.collider.gameObject.TryGetComponent<Resourse>(out Resourse resourse))
                {

                }
            }
        }
    }
}
