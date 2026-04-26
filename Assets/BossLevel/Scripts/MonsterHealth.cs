using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private Animator animator;

    public Slider healthBar;

    public GameObject crystalInScene;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

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

    if (crystalInScene != null)
    {
        crystalInScene.SetActive(true);
    }

    GetComponent<Collider>().enabled = false;
    this.enabled = false;
}
}