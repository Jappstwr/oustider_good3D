using UnityEngine;
using UnityEngine.InputSystem; 

public class FPSMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 15.0f;
    [SerializeField] private float _runSpeed = 25.0f;

    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _gravity = 20.0f;

    private Camera _mainCamera;
    private CharacterController _characterController;

    private InputAction _moveInput;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
