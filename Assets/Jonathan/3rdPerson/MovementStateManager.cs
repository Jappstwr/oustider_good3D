using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementStateManager : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 15.0f;
    [SerializeField] private float _runSpeed = 25.0f;
    [SerializeField] private float _crouchSpeed = 8.0f;

    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _gravity = 20.0f;

    private CharacterController _characterController;

    private InputAction _moveInput;
    private InputAction _runInput;

    private InputAction _crouchInput;

    private InputAction _jumpInput;
    private bool _jumped = false;

    [SerializeField] private float currentMoveSpeed;
    private Vector3 moveDirection = Vector3.zero;

    [Header("Script")]
    public bool canMove = true;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;



    void Start()
    {
        canMove = true; 

        _characterController = GetComponent<CharacterController>();

        _moveInput = InputSystem.actions.FindAction("Move");

        _runInput = InputSystem.actions.FindAction("Sprint");

        _crouchInput = InputSystem.actions.FindAction("Crouch");

        _jumpInput = InputSystem.actions.FindAction("Jump");
        _jumpInput.started += Jumped;

        currentMoveSpeed = _walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = _moveInput.ReadValue<Vector2>();
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());

        //For you Colin... These are bools which you can use for animations.  
        bool isMoving = moveVector.magnitude > 0.01f;
        bool isRunning = _runInput.IsPressed();
        bool isCrouching = _crouchInput.IsPressed();

        #region Animation Example
        //Such as this: 

        //_camAnim.SetBool("isWalk", isMoving && !isRunning && !isCrouching);

        //_camAnim.SetBool("isRun", isMoving && isRunning);

        //_camAnim.SetBool("isIdle", !isMoving && !isCrouching);

        //_camAnim.SetBool("isCrouch", !isMoving && isCrouching && !isRunning);

        //_camAnim.SetBool("isCrouchWalk", isMoving && isCrouching && !isRunning);
        #endregion


        if (!_characterController.isGrounded)
        {
            _jumped = false;

        }
        if (!canMove)
        {
            return;
        }

        if (_runInput.IsPressed())
        {
            currentMoveSpeed = _runSpeed;
        }
        else if (_crouchInput.IsPressed())
        {

            //transform.GetComponentInChildren<CapsuleCollider>().height = 2.0f;
            currentMoveSpeed = _crouchSpeed;
        }
        else
        {
            //transform.GetComponentInChildren<CapsuleCollider>().height = 8.25f;
            currentMoveSpeed = _walkSpeed;

        }


        HandleMovement(moveVector);
    }

    private void HandleMovement(Vector2 moveVector)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        float oldY = moveDirection.y;

        moveDirection = (forward * moveVector.y) + (right * moveVector.x);

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        moveDirection *= currentMoveSpeed;

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

    private void Jumped(InputAction.CallbackContext context)
    {
        _jumped = true;
    }
}
