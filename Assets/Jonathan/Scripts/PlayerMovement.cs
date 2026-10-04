using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 15.0f;
    [SerializeField] private float _runSpeed = 25.0f;

    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _gravity = 20.0f;

    [SerializeField] private float _lookSensitivity = 0.2f;
    [SerializeField] private float _lookAngleLimit = 85f;

    [SerializeField] private float _bobFrequency = 10.0f;
    [SerializeField] private float _bobAmount = 1f;
    [SerializeField] private float _bobSmoothSpeed = 10.0f;

    private float _bobTimer = 0f;
    private Vector3 _cameraStartingPos;

    private Camera _mainCamera;
    private CharacterController _characterController;

    private InputAction _moveInput;
    private InputAction _runInput;

    private InputAction _jumpInput;
    private bool _jumped = false;


    private float currentMoveSpeed;
    private Vector3 moveDirection = Vector3.zero;
    private float lookAngle = 0.0f;


    //PlayerInput playerInput;
    //InputAction moveAction;
    //Rigidbody rb;
    //public Animator _animation; 

    //public float speed = 5f;





    void Start()
    {


        _mainCamera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();

        _cameraStartingPos = _mainCamera.transform.localPosition;

        _moveInput = InputSystem.actions.FindAction("Move");
        _runInput = InputSystem.actions.FindAction("Sprint");
        _jumpInput = InputSystem.actions.FindAction("Jump");
        _jumpInput.started += Jumped;

        currentMoveSpeed = _walkSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //playerInput = GetComponent<PlayerInput>();
        //rb = GetComponent<Rigidbody>();
        //moveAction = playerInput.actions.FindAction("Move");
        //_animation = GetComponentInChildren<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = _moveInput.ReadValue<Vector2>();
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());



        if (!_characterController.isGrounded)
        {
            _jumped = false;
        }


        if (_runInput.WasPressedThisFrame())
        {
            currentMoveSpeed = _runSpeed;
        }
        else
        {
            currentMoveSpeed = _walkSpeed;
        }
        HandleMovement(moveVector);
        HandleLook(mouseDelta);
        HandleHeadBob(moveVector);
    }
    private void FixedUpdate()
    {
        //Vector2 moveInput = moveAction.ReadValue<Vector2>();


        //Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        //rb.linearVelocity = new Vector3(moveDir.x * speed, rb.linearVelocity.y, moveDir.z * speed);
    }

    private void HandleMovement(Vector2 moveVector)
    {
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        float oldY = moveDirection.y;

        Vector2 newSpeed = new Vector2(moveVector.y * currentMoveSpeed, moveVector.x * currentMoveSpeed);

        moveDirection = (forward * newSpeed.x) + (right * newSpeed.y);

        if (_jumped && _characterController.isGrounded)
        {
            moveDirection.y = _jumpForce;
        }
        else
        {
            moveDirection.y = oldY;
        }

        if (!_characterController.isGrounded)
        {
            moveDirection.y -= _gravity * Time.deltaTime;
        }
        _characterController.Move(moveDirection * Time.deltaTime);
    }
    private void HandleLook(Vector2 mouseDelta)
    {
        lookAngle += -mouseDelta.y * _lookSensitivity;
        lookAngle = Mathf.Clamp(lookAngle, -_lookAngleLimit, _lookAngleLimit);

        _mainCamera.transform.localRotation = Quaternion.Euler(lookAngle, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * _lookSensitivity, 0);
    }
    private void HandleHeadBob(Vector2 moveVector)
    {

        bool isMoving = moveVector.sqrMagnitude > 0.01f;

        if (_characterController.isGrounded && isMoving)
        {
            float speedMultiplier = (currentMoveSpeed / _walkSpeed);
            _bobTimer += Time.deltaTime * _bobFrequency * speedMultiplier;

            float bobY = Mathf.Sin(_bobTimer) * _bobAmount;
            float bobX = Mathf.Cos(_bobTimer * 0.5f) * (_bobAmount * 0.5f);

            Vector3 targetBobPos = _cameraStartingPos + new Vector3(bobX, bobY, 0);
            _mainCamera.transform.localPosition = Vector3.Lerp(_mainCamera.transform.localPosition, targetBobPos, Time.deltaTime * _bobSmoothSpeed);
        }
        else
        {
            _bobTimer = 0f;
            _mainCamera.transform.localPosition = Vector3.Lerp(_mainCamera.transform.localPosition, _cameraStartingPos, Time.deltaTime * _bobSmoothSpeed);
        }
    }

    private void Jumped(InputAction.CallbackContext context)
    {
        _jumped = true;
    }

}
