using UnityEngine;
using UnityEngine.InputSystem; 

public class Pickupable : MonoBehaviour
{
    private Rigidbody rb;
    public Transform _camera;
    public float maxDistance = 5;

    InputAction _pickupAction; 



    void Start()
    {
        _pickupAction = InputSystem.actions.FindAction("Interact"); 
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(_camera.position, _camera.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag("Pickupable"))
        {

            if (_pickupAction.WasPressedThisFrame())
            {
                Rigidbody rigid = hit.collider.gameObject.GetComponent<Rigidbody>();
                rigid.position = transform.position;
                hit.collider.gameObject.transform.SetParent(transform);
            }

            Debug.Log("Pickupable");
        }
    }
}
