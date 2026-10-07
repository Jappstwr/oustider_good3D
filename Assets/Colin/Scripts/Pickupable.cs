using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private Rigidbody rb;
    public Transform _camera;
    public float maxDistance = 5;
    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(_camera.position, _camera.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag("Pickupable"))
        {



            Debug.Log("Pickupable");
        }
    }
}
