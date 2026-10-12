using UnityEngine;

public class TriggerEndDialog : MonoBehaviour
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
        if (other.CompareTag("Cashier") && other.GetComponent<CashierBehavior>().readyForEnd && !triggered)
        {
            triggered = true;
            other.GetComponent<CashierBehavior>().reached = true;
            other.GetComponent<CashierBehavior>().StartEnd();
        }
    }
}
