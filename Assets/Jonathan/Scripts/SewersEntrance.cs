using UnityEngine;
using UnityEngine.SceneManagement;

public class SewersEntrance : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider hitbox)
    {
        Debug.Log("Something entered the sewer trigger!");

        if (hitbox.CompareTag("Player"))
        {
            Debug.Log("Loading scene!"); 
            SceneManager.LoadScene("SewerScene");
        }
    }

    
}
