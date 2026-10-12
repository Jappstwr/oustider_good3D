using UnityEngine;

public class Points : MonoBehaviour
{
    private bool reached = false;
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
        }
    }
}
