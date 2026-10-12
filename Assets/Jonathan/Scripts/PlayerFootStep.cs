using UnityEngine;

public class PlayerFootStep : MonoBehaviour
{
    public AudioSource _audioSource;

    public AudioClip _grass;
    public AudioClip _stone;

    RaycastHit hit; 
    public Transform RaycastStart;
    public float range;
    public LayerMask layerMask; 


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FootSteps()
    {
       if (Physics.Raycast(RaycastStart.position, RaycastStart.transform.up * -1, out hit, range, layerMask))
        {
            if (hit.collider.CompareTag("grass"))
            {
                PlayFootstepSoundL(_grass);
            }
            else if (hit.collider.CompareTag("stone"))
            {
                PlayFootstepSoundL(_stone);
            }
        }
    }
    void PlayFootstepSoundL(AudioClip audio)
    {
        _audioSource.pitch = Random.Range(0.8f, 1f); 
        _audioSource.PlayOneShot(audio); 
    }
}
