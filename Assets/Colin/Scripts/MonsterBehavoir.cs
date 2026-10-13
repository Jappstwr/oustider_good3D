using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehavoir : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chase,
        Wait,
        Search,
        Hunt
    }
    public Transform path;
    public Transform searchPath;
    private List<Transform> searchPoints = new List<Transform>();
    private List<Transform> points = new List<Transform>();
    public Transform teleportPoint;
    public Transform doorPoint;
    public Transform player;
    private Rigidbody rb;
    public float speed;
    public int currentPoint = -1;

    public float initialWaitTime;
    private float waitTime;

    public float rotationSpeed = 2f;
    public float maxMagnitudeChange = 1f;

    public float rayCastLenght;
    public int rayCastDegrees;
    public float rayCastDuration;

    public State state;
    void Start()
    {
        waitTime = initialWaitTime;
        state = State.Idle;
        rb = GetComponent<Rigidbody>();
        GetPoints();
        GetSearchPoints();
    }

    public void GetPoints()
    {
        for (int i = 0; i < path.childCount; i++)
        {
            points.Add(path.GetChild(i));
        }
    }
    public void GetSearchPoints()
    {
        for (int i = 0; i < searchPath.childCount; i++)
        {
            searchPoints.Add(searchPath.GetChild(i));
        }
    }

    void Update()
    {
        Vector3 currentDir = transform.forward;


        if (state == State.Chase)
        {
            float step = speed * Time.deltaTime;
            Transform target = path.GetChild(currentPoint + 1);
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        if (state == State.Search)
        {
            float step = speed * Time.deltaTime;
            Transform target = searchPath.GetChild(currentPoint + 1);
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            RaycastHit hit;
            for (float i = -rayCastDegrees; i < rayCastDegrees; i++)
            {
                if (Physics.Raycast(this.gameObject.transform.position, Quaternion.Euler(0, i, 0) * this.gameObject.transform.forward * rayCastLenght, out hit) && 
                    hit.collider.gameObject.CompareTag("Player"))
                {
                    state = State.Hunt;
                }
                Debug.DrawRay(this.gameObject.transform.position, Quaternion.Euler(0, i, 0) * this.gameObject.transform.forward * rayCastLenght, Color.red, rayCastDuration);
            }
        }

        if (state == State.Wait)
        {
            waitTime--;
            if (waitTime <= 0)
            {
                waitTime = initialWaitTime;
                state = State.Search;
            }
        }

        if (state == State.Hunt)
        {
            float step = speed * Time.deltaTime;
            Transform target = player;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        //if (state == State.Chase2)
        //{
        //    float step = speed * Time.deltaTime;
        //    Transform target = doorPoint;
        //    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        //}
    }
}
