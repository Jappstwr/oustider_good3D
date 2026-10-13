using UnityEngine;

public class TriggerBanter : MonoBehaviour
{
    private bool triggerd;
    public Transform Cashier;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cashier") && !triggerd)
        {
            triggerd = true;
            Cashier.GetComponent<CashierBehavior>().StartBanter();
        }
    }
}
