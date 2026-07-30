using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBuilding : MonoBehaviour
{
    [Header("Building Settings")]
    private GameObject _currentBuild;
    [SerializeField] private float maxBuildDestance;
    [SerializeField] private float surfaceAngleLimit = 30f;
    [SerializeField] private float collisionCheckRadius = 1f;
    [SerializeField] private LayerMask buildLayer;
    [SerializeField] private LayerMask obstacleLayer;
    private bool _canBuild = false;
    [Header("Materials")]
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;

    [Header("Input")]

    [SerializeField] private InputActionReference buildKey;

    private Camera _camera;
    private GameObject _previewInstance;

    [Header("References")]
    [SerializeField] private inventoryController inventoryController;

    private void Start()
    {
        _camera = Camera.main;
        
    }

    public void NewBuild(GameObject buildPrefab)
    {
        _currentBuild = buildPrefab; 
        _previewInstance = Instantiate(_currentBuild);
        _previewInstance.SetActive(false);
        for (int i = 0; i < _previewInstance.transform.childCount; i++)
        {
            if (_previewInstance.transform.GetChild(i).TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
            {
                mesh.gameObject.SetActive(false);
            }
        }
        _previewInstance.GetComponent<Collider>().enabled = false;
    }
    public void ExidBuildMode()
    {
        _currentBuild = null;
        Destroy(_previewInstance);
    }

    private void Update()
    {
        if (_currentBuild)
        {
            HandleInput();
            HandleBuildPreview();
        }
    }

    private void HandleBuildPreview()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if(Physics.Raycast(ray,out RaycastHit hit,maxBuildDestance,buildLayer))
        {
            Vector3 position = hit.point;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up,hit.normal);

            if(Vector3.Angle(hit.normal,Vector3.up) < surfaceAngleLimit && !Physics.CheckSphere(position,collisionCheckRadius,obstacleLayer))
            {
                _previewInstance.SetActive(true);
                _previewInstance.transform.SetPositionAndRotation(position, rotation);
                SetPreviewColor(greenMaterial);
                _canBuild = true;
            }
            else
            {
                _previewInstance.SetActive(true);
                _previewInstance.transform.SetPositionAndRotation(position, rotation);
                SetPreviewColor(redMaterial);
                _canBuild = false;
            }
        }
        else
        {
            _previewInstance.SetActive(false) ;
        }

    }
    private void HandleInput()
    {
        if (!_previewInstance.activeSelf) return;
        if(buildKey.action.WasPerformedThisFrame() && _canBuild)
        {
            Instantiate(_currentBuild, _previewInstance.transform.position,_previewInstance.transform.rotation,null);
            inventoryController.MunisCurrentSelection();
        }
    }

    private void SetPreviewColor(Material newColor)
    {
        foreach(MeshRenderer meshRenderer in _previewInstance.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = newColor;
        }
    }
}
