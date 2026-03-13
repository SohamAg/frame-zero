using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public TMP_Text inventoryText;

    private List<string> items = new List<string>();

    void Start()
    {
        UpdateUI();
    }

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        UpdateUI();
    }

    public int GetItemCount(string itemName)
    {
        int count = 0;
        foreach (var item in items)
        {
            if (item == itemName) count++;
        }
        return count;
    }

    public void RemoveItems(string itemName, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            items.Remove(itemName);
        }
        UpdateUI();
    }

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