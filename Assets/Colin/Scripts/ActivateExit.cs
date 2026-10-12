using UnityEngine;

public class ActivateExit : MonoBehaviour
{
    public Transform exit;
    public Transform blockade;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            exit.GetComponent<BoxCollider>().enabled = true;
            blockade.position = Vector3.zero;
        }
    }
}
