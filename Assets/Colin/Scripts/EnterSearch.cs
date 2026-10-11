using UnityEngine;

public class EnterSearch : MonoBehaviour
{
    bool reached = false;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Monster") && !reached)
        {
            reached = true;
            other.GetComponent<MonsterBehavoir>().currentPoint = -1;
            other.GetComponent<MonsterBehavoir>().state = MonsterBehavoir.State.Search;
        }
    }
}
