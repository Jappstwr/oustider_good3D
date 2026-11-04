using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    [HideInInspector] public Vector3 dir;
    float hzInput, vzInput;
    CharacterController controller; 

    void Start()
    {
        controller = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
