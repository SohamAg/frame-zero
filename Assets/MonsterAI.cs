using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;

    public float sightRange = 10f;
    public float attackRange = 2f;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > sightRange)
        {
            Idle();
        }
        else if (distance <= sightRange && distance > attackRange)
        {
            Chase();
        }
        else if (distance <= attackRange)
        {
            Attack();
        }
    }

    void Idle()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    void Chase()
    {
        agent.SetDestination(player.position);
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
    }
}