using UnityEngine;

public class CashierBehavior : MonoBehaviour
{
    private AudioSource source;

    public AudioClip welcome;
    public AudioClip checkout;
    public AudioClip banter;
    public AudioClip endPoint;
    public float volume;
    private bool ready = false;
    public bool reached = false;
    public bool readyForEnd = false;
    private float timeAtStart;

    private Vector3 initialPos;
    public Transform banterPos;
    void Start()
    {
        source = GetComponent<AudioSource>();
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready && Time.time > timeAtStart + checkout.length && !readyForEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, banterPos.position, 1f);
        }

        if (readyForEnd && Time.time > timeAtStart + banter.length && !reached)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, 1f);
        }
    }

    public void StartWelcome()
    {
        source.PlayOneShot(welcome);
    }

    public void StartCheckout()
    {
        timeAtStart = Time.time;
        source.PlayOneShot(checkout);
        ready = true;
    }

    public void StartBanter()
    {
        timeAtStart = Time.time;
        source.PlayOneShot(banter);
        readyForEnd = true;
    }

    public void StartEnd()
    {
        source.PlayOneShot(endPoint);
    }
}
