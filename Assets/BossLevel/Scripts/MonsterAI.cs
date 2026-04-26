using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;

    public float sightRange = 10f;
    public float attackRange = 2f;

    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    private NavMeshAgent agent;
    private Animator animator;

    private MonsterHealth health;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<MonsterHealth>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetPaused(bool paused)
    {
        if (agent != null)
        {
            agent.isStopped = paused;
            agent.velocity = Vector3.zero;
        }

        animator.SetFloat("Speed", 0);

        this.enabled = !paused;
    }

    void Update()
    {
        if (player == null || health == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > sightRange)
        {
            agent.SetDestination(transform.position);
            animator.SetFloat("Speed", 0);
        }
        else if (distance > attackRange)
        {
            agent.SetDestination(player.position);
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            agent.SetDestination(transform.position);
            animator.SetFloat("Speed", 0);

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                int attackType = Random.Range(1, 4);

                if (attackType == 1)
                    animator.SetTrigger("Attack1");
                else if (attackType == 2)
                    animator.SetTrigger("Attack2");
                else
                    animator.SetTrigger("Attack3");

                lastAttackTime = Time.time;
            }
        }
    }
}