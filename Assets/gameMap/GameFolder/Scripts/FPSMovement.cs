using UnityEngine;
using UnityEngine.InputSystem; 

public class FPSMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 15.0f;
    [SerializeField] private float _runSpeed = 25.0f;
    [SerializeField] private float _crouchSpeed = 8.0f; 

    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _gravity = 20.0f;

    private Camera _mainCamera;
    private CharacterController _characterController;

    private InputActionReference _moveAction;
    private Vector2 _moveInput;


    private void OnEnable()
    {
        //_moveAction.action.performed += 
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();    
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(); 
    }

    private void HandleMovement()
    {

    }
}
