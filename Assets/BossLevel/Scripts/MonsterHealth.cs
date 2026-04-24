using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        // Send health to Animator
        animator.SetFloat("Health", currentHealth);
    }

    void Die()
    {
        animator.SetTrigger("Death");
    }
}