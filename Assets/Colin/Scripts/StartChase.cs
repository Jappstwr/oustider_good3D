using UnityEngine;

public class StartChase : MonoBehaviour
{
    public Transform monster;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monster.GetComponent<MonsterBehavoir>().state = MonsterBehavoir.State.Chase;
        }
    }
}
