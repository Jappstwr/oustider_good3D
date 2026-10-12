using UnityEngine;

public class TriggerCheckout : MonoBehaviour
{
    private bool triggered;
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
        if (other.CompareTag("Pickupable") && !triggered)
        {
            triggered = true;
            Cashier.GetComponent<CashierBehavior>().StartCheckout();
        }
    }
}
