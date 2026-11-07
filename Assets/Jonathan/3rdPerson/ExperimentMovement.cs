using UnityEngine;
using Unity.Collections;
using UnityEngine.AI;

public enum ExperimentState
{
    Dormant,
    Awakening, 
    Standing, 
    Chasing, 
    Attacking, 
    Dead
}

[RequireComponent(typeof(NavMeshAgent))]
public class ExperimentMovement : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Targetting & Detection")]
    public Transform playerTransform;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    public float detectionRange = 12f;
    public float visionAngle = 90f; 
    public float wakeUpRadius = 3f; 
    [Range(0f, 1f)] public float wakeUpChanceFromMovement = 0.35f;

    [Header("Combat & Timing")]
    public float attackDistance = 2f;
    public float attackInterval = 1.5f;
    private float lastAttackTime;

    [Header("State")]
    public ExperimentState currentState;
    

    NavMeshAgent agent;
    public Animator animator; 


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (playerTransform == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player"); 
            if (p != null)
            {
                playerTransform = p.transform; 
            }
        }

        InitializeRandomState();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == ExperimentState.Dead || playerTransform == null) return;

        switch (currentState)
        {
            case ExperimentState.Dormant:
                CheckDormantAwakening();
                break;

            case ExperimentState.Standing:
                LookForPlayer();
                break;

            case ExperimentState.Chasing:
                HandleChasing();
                break;

            case ExperimentState.Attacking:
                HandleAttacking();
                break;
        }


    }

    private void InitializeRandomState()
    {
        float roll = Random.value;

        if (roll < 0.5f)
        {
            SetState(ExperimentState.Dormant);
        }
        else
        {
            SetState(ExperimentState.Standing);
        }
    }

    private void SetState(ExperimentState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case ExperimentState.Dormant:
                if (agent.enabled) agent.isStopped = true;
                if (animator) animator.SetBool("IsDormant", true);
                break;

            case ExperimentState.Awakening:
                if (agent.enabled) agent.isStopped = true;
                if (animator) animator.SetTrigger("Awake");
                // Delay chase until get-up animation finishes (e.g. 2s)
                Invoke(nameof(FinishAwakening), 2.0f);
                break;

            case ExperimentState.Standing:
                if (agent.enabled) agent.isStopped = true;
                if (animator) animator.SetBool("IsDormant", false);
                break;

            case ExperimentState.Chasing:
                if (agent.enabled)
                {
                    agent.isStopped = false;
                    agent.SetDestination(playerTransform.position);
                }
                if (animator) animator.SetBool("IsChasing", true);
                break;

            case ExperimentState.Dead:
                if (agent.enabled) agent.isStopped = true;
                if (animator) animator.SetTrigger("Die");
                GetComponent<Collider>().enabled = false;
                this.enabled = false;
                break;
        }
    }
    private void CheckDormantAwakening()
    {
        float distToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distToPlayer <= wakeUpRadius)
        {
            // Random chance check when player enters proximity zone
            if (Random.value <= wakeUpChanceFromMovement)
            {
                SetState(ExperimentState.Awakening);
            }
        }
    }

    private void LookForPlayer()
    {
        Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;
        float distToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distToPlayer <= detectionRange)
        {
            // Check cone of vision angle
            float angle = Vector3.Angle(transform.forward, dirToPlayer);
            if (angle < visionAngle / 2f)
            {
                // Raycast sight check (makes sure walls block vision)
                if (!Physics.Raycast(transform.position + Vector3.up, dirToPlayer, distToPlayer, obstacleLayer))
                {
                    SetState(ExperimentState.Chasing);
                }
            }
        }
    }

    public void HearSound(Vector3 soundPosition, float soundRadius)
    {
        if (currentState == ExperimentState.Dead) return;

        float distToSound = Vector3.Distance(transform.position, soundPosition);
        if (distToSound <= soundRadius)
        {
            if (currentState == ExperimentState.Dormant)
            {
                SetState(ExperimentState.Awakening);
            }
            else if (currentState == ExperimentState.Standing)
            {
                SetState(ExperimentState.Chasing);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player physical collision bump while playing dead triggers immediate wake up
        if (currentState == ExperimentState.Dormant && other.CompareTag("Player"))
        {
            SetState(ExperimentState.Awakening);
        }
    }

    private void FinishAwakening()
    {
        if (currentState == ExperimentState.Awakening)
        {
            SetState(ExperimentState.Chasing);
        }
    }

    private void HandleChasing()
    {
        float distToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distToPlayer <= attackDistance)
        {
            SetState(ExperimentState.Attacking);
        }
        else
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    private void HandleAttacking()
    {
        agent.isStopped = true;
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

        float distToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distToPlayer > attackDistance + 0.5f)
        {
            SetState(ExperimentState.Chasing);
            return;
        }

        if (Time.time >= lastAttackTime + attackInterval)
        {
            lastAttackTime = Time.time;
            if (animator) animator.SetTrigger("Attack");
            // Add damage dealing logic to player here
        }
    }

    public void ApplyDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentState == ExperimentState.Dormant)
        {
            SetState(ExperimentState.Awakening);
        }
        else if (currentState == ExperimentState.Standing)
        {
            SetState(ExperimentState.Chasing);
        }

        if (currentHealth <= 0f)
        {
            SetState(ExperimentState.Dead);
        }
    }
}
