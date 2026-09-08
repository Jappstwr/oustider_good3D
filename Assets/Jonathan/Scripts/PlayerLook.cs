using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerLook : MonoBehaviour
{
    PlayerInput playerInput;

    InputAction rotationAction;

    public Transform cameraTransform;
    public float sensitivity = 0.2f;

    float rotationX; 



    void Start()
    {
        playerInput = GetComponent<PlayerInput>(); 
        rotationAction = playerInput.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseDelta = rotationAction.ReadValue<Vector2>();

        transform.Rotate(Vector3.up * mouseDelta.x * sensitivity);

        rotationX -= mouseDelta.y * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -85f, 85f);

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

    }
}
