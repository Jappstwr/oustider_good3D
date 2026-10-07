using UnityEngine;
using UnityEngine.InputSystem; 

public class Pickupable : MonoBehaviour
{
    private bool holdingItem;
    private Rigidbody rb;
    public Transform player;
    public Transform _camera;
    public float maxDistance = 5;
    private float cooldown;

    InputAction _pickupAction; 



    void Start()
    {
        _pickupAction = InputSystem.actions.FindAction("Interact"); 
    }

    void Update()
    {
        cooldown--;

        if (holdingItem && _pickupAction.WasPressedThisFrame() && cooldown <= 0)
        {
            transform.SetParent(null);
            transform.GetComponent<Rigidbody>().useGravity = true;
            transform.GetComponent<Rigidbody>().isKinematic = false;
            holdingItem = false;
            cooldown = 10;
        }



        RaycastHit hit;
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag("Pickupable") && !holdingItem && cooldown <= 0)
        {
            if (_pickupAction.WasPressedThisFrame())
            {
                transform.SetParent(player,false);
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.GetComponent<Rigidbody>().isKinematic = true;
                transform.localPosition = new Vector3(0, 0, 0);
                transform.localRotation = Quaternion.identity;
                holdingItem = true;
                cooldown = 10;
            }
        }
    }

    public void Grab()
    {
        Transform holdPoint = transform.GetChild(0).GetChild(0);
        RaycastHit hit;
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag("Pickupable") && !holdingItem)
        {

            if (_pickupAction.WasPressedThisFrame())
            {
                Transform obj = hit.collider.gameObject.transform;
                Rigidbody rigid = hit.collider.gameObject.GetComponent<Rigidbody>();
                hit.collider.gameObject.transform.SetParent(holdPoint, false);

                obj.GetComponent<Rigidbody>().isKinematic = true;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;

                holdingItem = true;
            }
        }
    }

    public void Drop()
    {
        if (holdingItem && _pickupAction.WasPressedThisFrame())
        {
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                if (transform.GetChild(i).CompareTag("Pickupable"))
                {
                    transform.GetChild(i).transform.SetParent(null);
                    holdingItem = false;
                }
            }
        }
    }
}
