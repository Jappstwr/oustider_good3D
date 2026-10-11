using UnityEngine;

public class DogMovement : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] GameObject[] waypoints;
    [SerializeField] private int stopAtWaypoint = 3; 

    [Header("Dog Settings")]
    [SerializeField] private float walkSpeed = 16f;
    [SerializeField] private float currentSpeed; 

    public Rigidbody rb;

    private int currentWayPoint = 0;
    [SerializeField] private bool isWaiting = false;
    [SerializeField] private bool canContinue = true; 
        


    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!canContinue)
        {
            return; 
        }   

        if (!isWaiting)
        {
            FollowWayPoints(); 
        }
    }

    private void FollowWayPoints()
    {
        if (currentWayPoint >= waypoints.Length)
        {
            return; 
        }

        Transform target = waypoints[currentWayPoint].transform;

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        currentSpeed = direction.magnitude; 

        if (direction.magnitude >= 0.2f)
        {
            Vector3 movement = direction.normalized * walkSpeed * Time.deltaTime;

            rb.MovePosition(rb.position + movement);

            //transform.forward = direction.normalized; 

            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);

        }
        else
        {
            if (currentWayPoint == stopAtWaypoint)
            {
                isWaiting = true; 
            }
            else
            {
                currentWayPoint++; 
            }
        }
    }

    public void ContinueWalk()
    {
        isWaiting = false;
        currentWayPoint++; 
    }
}
