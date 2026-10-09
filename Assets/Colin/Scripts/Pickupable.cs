using UnityEngine;
using UnityEngine.InputSystem; 

public class Pickupable : MonoBehaviour
{
    public bool holdingItem;
    private Rigidbody rb;
    private Transform player;
    private Transform _camera;
    public float maxDistance = 5;
    private float cooldown;
    private Transform item;

    InputAction _pickupAction; 



    void Start()
    {
        _pickupAction = InputSystem.actions.FindAction("Interact"); 
        player = transform.GetChild(0).GetChild(0);
        _camera = transform.GetChild(1).GetChild(0);
    }

    void Update()
    {
        cooldown--;
        Drop();
        Grab();
        Interactable();

    }

    public void Grab()
    {
        Transform holdPoint = transform.GetChild(0).GetChild(0);
        RaycastHit hit;
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag("Pickupable") && !holdingItem && cooldown <= 0)
        {

            if (_pickupAction.WasPressedThisFrame())
            {
                Transform obj = hit.collider.gameObject.transform;
                Rigidbody rigid = hit.collider.gameObject.GetComponent<Rigidbody>();
                hit.collider.gameObject.transform.SetParent(holdPoint, false);

                obj.GetComponent<Rigidbody>().isKinematic = true;
                obj.GetComponent<Rigidbody>().useGravity = false;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;

                item = obj;

                holdingItem = true;
                cooldown = 10;
            }
        }
    }

    public void Drop()
    {
        if (holdingItem && _pickupAction.WasPressedThisFrame() && cooldown <= 0)
        {
            item.SetParent(null);
                    item.GetComponent<Rigidbody>().isKinematic = false;
                    item.GetComponent<Rigidbody>().useGravity = true;
                    holdingItem = false;
                    cooldown = 10;
        }
    }

    public void Interactable()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.position, _camera.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag("Interactable") && _pickupAction.WasPressedThisFrame())
        {
            hit.collider.GetComponent<WalletLogic>().Interacted();
        }
    }
}
