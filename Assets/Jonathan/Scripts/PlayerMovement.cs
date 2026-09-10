using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    Rigidbody rb;
    public Animator _animation; 

    

    public float speed = 5f; 


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        moveAction = playerInput.actions.FindAction("Move");
        _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();


        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        rb.linearVelocity = new Vector3(moveDir.x * speed, rb.linearVelocity.y, moveDir.z * speed);

        if (moveInput.magnitude > 0f)
        {
            _animation.SetBool("isWalk", true); 
        }
        else 
        {
            _animation.SetBool("isWalk", false); 
        }
    }
}
