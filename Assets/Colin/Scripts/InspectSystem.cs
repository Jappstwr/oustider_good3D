using UnityEngine;
using UnityEngine.InputSystem;

public class InspectSystem : MonoBehaviour
{
    public Transform objectToInspect;

    public float rotationSpeed = 0.5f;

    private Vector2 previousMousePosition;

    private InputAction drag;
    private InputAction exit;

    public Transform InspectionCamera;
    public Transform PlayerCamera;
    void Start()
    {
        //drag = InputSystem.actions.FindAction("Drag");
        drag = transform.GetComponent<PlayerInput>().actions.FindAction("Drag");
        exit = transform.GetComponent<PlayerInput>().actions.FindAction("Exit");
    }

    // Update is called once per frame
    void Update()
    {
        if (InspectionCamera.GetComponent<Camera>().enabled)
        {
            if (drag.WasPressedThisFrame())
            {
                previousMousePosition = Mouse.current.position.ReadValue();
            }

            if (drag.IsPressed())
            {
                Vector2 deltaMousePosition = Mouse.current.position.ReadValue() - previousMousePosition;
                float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
                float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

                Quaternion horizontalRotation = Quaternion.AngleAxis(-deltaMousePosition.x * rotationSpeed, InspectionCamera.up);
                Quaternion verticalRotation = Quaternion.AngleAxis(deltaMousePosition.y * rotationSpeed, InspectionCamera.right);

                objectToInspect.rotation = horizontalRotation * verticalRotation * objectToInspect.rotation;

                previousMousePosition = Mouse.current.position.ReadValue();
            }
            if (exit.WasPressedThisFrame())
            {
                PlayerCamera.GetComponent<Camera>().enabled = true;
                InspectionCamera.GetComponent<Camera>().enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
