using UnityEngine;
using UnityEngine.Animations; 
public class PressurePlateCabinet : MonoBehaviour
{
    [SerializeField] private Animator _cabinetAnim;
    





    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit pressure plate"); 

        if (other.CompareTag("Player"))
        {
            Debug.Log("Activating animation");

            _cabinetAnim.SetBool("isEnabled", true); 
        }
        else
        {
            _cabinetAnim.SetBool("isIdle", true); 
        }
    }

    //private void OnCollisionEnter(Collision hitbox)
    //{
    //    if (hitbox.gameObject.CompareTag("Player"))
    //    {
    //        _cabinetAnim.SetTrigger("isEnabled"); 
    //    }
    //}
}
