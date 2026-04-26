using UnityEngine;

public class Crystal : MonoBehaviour
{
    [Header("Crystal Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    public string itemName = "Shards";

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnMouseDown()
    {
        Hit();
    }

    public void Hit()
    {
        currentHealth--;

        Debug.Log(gameObject.name + " hit! Remaining: " + currentHealth);

        if (currentHealth <= 0)
        {
            Break();
        }
    }

    void Break()
    {
        // Add to inventory
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(itemName);
        }
        else
        {
            Debug.LogWarning("InventoryManager not found!");
        }

        // Destroy crystal
        Destroy(gameObject);
    }
}