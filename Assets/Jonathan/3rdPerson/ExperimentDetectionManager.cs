using UnityEngine;

public enum BodyParts { Head, Torso, Limb}
public class ExperimentDetectionManager : MonoBehaviour
{

    public BodyParts bodyParts;
    public ExperimentMovement mainScript;
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float baseDamage)
    {
        if (mainScript == null)
        {
            return; 
        }

        float multiplier = 1f;

        switch (bodyParts)
        {
            case BodyParts.Head:
                multiplier = 999f;
                break;
            case BodyParts.Torso:
                multiplier = 0.5f;
                break;
            case BodyParts.Limb:
                multiplier = 0.25f;
                break; 
            
        }
        mainScript.ApplyDamage(baseDamage * multiplier);
    }
}
