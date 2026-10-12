using UnityEngine;

public class ShelfFall : MonoBehaviour
{
    public Animator fall;
    public Transform hitBox;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            fall.SetBool("Fall", true);
            hitBox.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
