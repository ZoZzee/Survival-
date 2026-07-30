using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private float p_moveSpeed = 0f;
    [SerializeField]private float p_walkSpeed = 5f;
    [SerializeField]private float p_runSpeed = 10f;
    [SerializeField]private float p_rotationSpeed = 10f;
    [SerializeField] private Transform _cameraTramsform;
    [SerializeField] private float _cameraLimit = 80f;
    [SerializeField] private float p_jumpForse;
    [SerializeField] private float p_gravity;
    private float p_verticalVelocity;
    public bool isRunning;

    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private NeedsManager _needsManager;
    [SerializeField] private Animator _handAnimator;
    private SaveSystem _saveSystem;

    [Header("Input")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _lookAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _runAction;

    private Vector2 p_moveInput;
    private Vector2 p_lookInput;

    private float p_cameraPitch;

    private bool _canMoov = true;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        _saveSystem.playerInfo.position = transform.position;
    }
    private void Load()
    {
        _canMoov = false;
        transform.position = _saveSystem.playerInfo.position;
        Invoke(nameof(CanMove), 0.1f);
    }

    private void CanMove()
    {
        _canMoov = true;
    }

    private void Update()
    {
        p_moveInput = _moveAction.action.ReadValue<Vector2>();
        p_lookInput = _lookAction.action.ReadValue<Vector2>();

        HandleMovement(p_moveInput);
        HandleLook(p_lookInput);
    }

    private void HandleMovement(Vector2 moveInput)
    {
        if (_canMoov)
        {
            Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
            direction = Quaternion.Euler(0f, _cameraTramsform.eulerAngles.y, 0f) * direction;
            direction.y = 0f;
            direction.Normalize();

            if (_characterController.isGrounded)
            {
                p_verticalVelocity = -1f;

                if (_jumpAction.action.triggered)
                {
                    p_verticalVelocity = p_jumpForse;
                }
            }
            else
            {
                p_verticalVelocity += p_gravity * Time.deltaTime;
            }

            if (_runAction.action.IsPressed() && _needsManager.Energy.CanRun)
            {
                p_moveSpeed = p_runSpeed;
                _needsManager.Running();
            }
            else
            {
                p_moveSpeed = p_walkSpeed;
            }

            Vector3 velocity = direction * p_moveSpeed;
            velocity.y = p_verticalVelocity;
            _handAnimator.SetFloat("Velocity", velocity.magnitude);
            _characterController.Move(velocity * Time.deltaTime);
        }
        else
        {
            _handAnimator.SetFloat("Velocity", 0);
        }
    }

    private void HandleLook(Vector2 lookInput)
    {
        transform.Rotate(Vector3.up * lookInput.x * p_rotationSpeed * Time.deltaTime);
        p_cameraPitch -= lookInput.y * p_rotationSpeed * Time.deltaTime;
        p_cameraPitch = Mathf.Clamp(p_cameraPitch, -_cameraLimit, _cameraLimit);
        _cameraTramsform.localEulerAngles = new Vector3(p_cameraPitch,0f,0f); 
    }

}
