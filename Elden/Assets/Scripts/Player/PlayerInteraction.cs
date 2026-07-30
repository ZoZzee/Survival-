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
    [SerializeField] private Effects UI_Effects;

    [Header("Referenses")]
    [SerializeField] private NeedsManager _needsManager;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private inventoryController _inventoryController;

    [Header("Input")]
    [SerializeField] private InputActionReference _interactAction;
    [SerializeField] private InputActionReference _useToolAction;

    private Camera mainCamera;
    private void Start()
    {
        _needsManager = NeedsManager.instance;
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
                    _inventoryController.RefreshSelection();
                    Destroy(hit.collider.gameObject);
                }
            }
            if (hit.collider.gameObject.TryGetComponent<SubjectInteraction>(out SubjectInteraction subjectInteraction))
            {
                _interactionText.text = subjectInteraction.subject.subjectName;
                if (_interactAction.action.triggered)
                {
                    if (subjectInteraction.subject.subjectName == "Bed")
                    {
                        UI_Effects.Sleep();
                        _needsManager.Sleeping(subjectInteraction.subject);
                    }

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
                if (hitTool.collider.gameObject.TryGetComponent<Resourse>(out Resourse resourse))
                {
                    resourse.TryHit(_inventoryController.currentTool.tool,_playerInventory, hitTool.point);
                }
            }
        }
    }

    //private Coroutine
    //{

    //}
}
