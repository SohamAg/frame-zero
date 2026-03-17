using UnityEngine;
using UnityEngine.AI;

public class WizardAI : MonoBehaviour
{
    public enum WizardState
    {
        Idle,
        Patrolling
    }

    [Header("State")]
    public WizardState currentState = WizardState.Patrolling;

    [Header("Player Detection")]
    public Transform player;
    public float detectionDistance = 5f;

    [Header("Patrol Settings")]
    public float wanderRadius = 10f;
    public float patrolTimer = 5f;

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = patrolTimer;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionDistance)
        {
            SwitchState(WizardState.Idle);
        }
        else
        {
            SwitchState(WizardState.Patrolling);
        }

        switch (currentState)
        {
            case WizardState.Idle:
                HandleIdle();
                break;

            case WizardState.Patrolling:
                HandlePatrol();
                break;
        }
    }

    void HandleIdle()
    {
        if (!agent.isStopped)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        Vector3 lookDir = (player.position - transform.position).normalized;
        lookDir.y = 0;

        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(lookDir),
                5f * Time.deltaTime
            );
        }
    }
    void HandlePatrol()
    {
        if (agent.isStopped) {
            agent.isStopped = false;
        }
        timer += Time.deltaTime;

        if (timer >= patrolTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    void SwitchState(WizardState newState)
    {
        if (currentState == newState) {
            return;
        }
        currentState = newState;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, NavMesh.AllAreas);

        return navHit.position;
    }
}