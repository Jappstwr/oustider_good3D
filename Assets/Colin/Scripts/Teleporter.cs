using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private bool reached = false;
    public Transform teleportPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Monster") && !reached)
        {
            reached = true;
            other.GetComponent<MonsterBehavoir>().currentPoint++;
            other.transform.position = teleportPoint.position;
            other.GetComponent<MonsterBehavoir>().speed = 10;
        }
    }
}
