using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public int damage = 20;

    private PlayerHealth playerHealth;
    private Animator animator;

    public float damageDelay = 0.4f; // when hit happens in animation
    public float damageWindow = 0.2f;

    private bool canDealDamage = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        playerHealth = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Detect if ANY attack animation is playing
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsTag("Attack"))
        {
            // simple timing window
            float normalizedTime = state.normalizedTime;

            if (normalizedTime >= damageDelay && normalizedTime <= damageDelay + damageWindow)
            {
                canDealDamage = true;
            }
            else
            {
                canDealDamage = false;
            }
        }
        else
        {
            canDealDamage = false;
        }
    }

    public void TryDealDamage()
    {
        if (!canDealDamage) return;

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        // prevent multi-hit spam
        canDealDamage = false;
    }
}