using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations; 

public class CashierScript : MonoBehaviour
{
    [SerializeField] private Animator _cashierAnim;
    [SerializeField] private GameObject _cashRegister;


    private bool canTriggered = false; 


    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision hitbox)
    //{
    //    if (hitbox.gameObject.CompareTag("Pickupable"))
    //    {
    //        Debug.Log("Loaf has been detected"); 
    //        _cashierAnim.SetTrigger("walk");
    //    }
    //    else
    //    {
    //        _cashierAnim.SetTrigger("idle"); 
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Register"))
        {
            Debug.Log("Loaf has been detected");
            _cashierAnim.SetBool("isWalk", true);
        }
        else
        {
            _cashierAnim.SetBool("isIdle", true);
        }
    }

}