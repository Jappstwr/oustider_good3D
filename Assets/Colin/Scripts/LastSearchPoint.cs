using UnityEngine;

public class LastSearchPoint : MonoBehaviour
{
    public bool reached = false;
    public Transform lastpoint;
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
            other.GetComponent<MonsterBehavoir>().currentPoint = -1;

            if (lastpoint.GetComponent<SearchPoints>() == null)
            {
                lastpoint.GetComponent<LastSearchPoint>().reached = false;
            }
            else
            {
                lastpoint.GetComponent<SearchPoints>().reached = false;
            }

            other.GetComponent<MonsterBehavoir>().state = MonsterBehavoir.State.Wait;
        }
    }
}
