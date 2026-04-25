using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int potionHeal = 20;
    private bool potionUsed = false;

    private PlayerDefense defense;

    void Start()
    {
        currentHealth = maxHealth;
        defense = GetComponent<PlayerDefense>();
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

        Debug.Log("Potion used! Health: " + currentHealth);
    }

    void Die()
    {
        Debug.Log("Player died");
    }
}