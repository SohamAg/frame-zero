using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public TMP_Text inventoryText; // Assign InventoryText here

    private List<string> items = new List<string>();

    void Start()
    {
        UpdateUI();
    }

    // Call this to add an item
    public void AddItem(string itemName)
    {
        items.Add(itemName);
        UpdateUI();
    }

    // Refresh the UI
    void UpdateUI()
    {
        if (inventoryText == null) return;

        if (items.Count == 0)
        {
            inventoryText.text = "Inventory: (empty)";
            return;
        }

        inventoryText.text = "Inventory:\n";
        foreach (string item in items)
        {
            inventoryText.text += "- " + item + "\n";
        }
    }
}