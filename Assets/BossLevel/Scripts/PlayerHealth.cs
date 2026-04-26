using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int potionHeal = 20;
    private bool potionUsed = false;

    private PlayerDefense defense;

    public Slider healthBar;

    public GameObject gameOverCanvas;

    private bool isDead = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        defense = GetComponent<PlayerDefense>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UsePotion();
        }
    }

    public void TakeDamage(int damage)
    {
        // Block damage if defending
        if (defense != null && defense.isDefending)
        {
            Debug.Log("Blocked damage!");
            return;
        }

        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UsePotion()
    {
        if (potionUsed)
        {
            Debug.Log("Potion already used");
            return;
        }

        currentHealth += potionHeal;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        potionUsed = true;

        if (healthBar != null)
            healthBar.value = currentHealth;

        Debug.Log("Potion used! Health: " + currentHealth);


        if (animator != null)
        {
            animator.SetTrigger("CastSpell");
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player died");

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

    }
}