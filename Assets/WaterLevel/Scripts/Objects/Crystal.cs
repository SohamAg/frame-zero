using UnityEngine;

public class Crystal : MonoBehaviour
{
    [Header("Crystal Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    public string itemName = "Crystal";

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Called when player clicks directly on the crystal
    void OnMouseDown()
    {
        Hit();
    }

    // Called when player hits the crystal (can also be used by raycast)
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