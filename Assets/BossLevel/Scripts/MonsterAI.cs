using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;

    public float sightRange = 10f;
    public float attackRange = 2f;

    private NavMeshAgent agent;
    private Animator animator;

    public GameObject levelCompleteCanvas;
    public int health = 100;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > sightRange)
        {
            Idle();
        }
        else if (distance > attackRange)
        {
            Chase();
        }
        else
        {
            Attack();
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void Idle()
    {
        agent.SetDestination(transform.position);
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);
        animator.SetTrigger("Attack");

        // attempt damage (will only work in hit window)
        GetComponent<MonsterAttack>().TryDealDamage();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // random hit reaction
        int hitType = Random.Range(1, 3);

        if (health > 0)
        {
            if (hitType == 1)
                animator.SetTrigger("Hit1");
            else
                animator.SetTrigger("Hit2");
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        agent.enabled = false;

        if (levelCompleteCanvas != null)
            levelCompleteCanvas.SetActive(true);

        gameObject.SetActive(false);
    }
}