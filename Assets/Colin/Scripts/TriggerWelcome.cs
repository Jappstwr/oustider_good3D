using UnityEngine;

public class TriggerWelcome : MonoBehaviour
{
    private bool triggered;
    public Transform cashier;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            cashier.GetComponent<CashierBehavior>().StartWelcome();
        }
    }
}
