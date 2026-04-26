using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public TMP_Text inventoryText;
    public static InventoryManager Instance;

    private Dictionary<string, int> items = new Dictionary<string, int>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }

        UpdateUI();
    }

    public int GetItemCount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }

    public void RemoveItems(string itemName, int quantity)
    {
        if (!items.ContainsKey(itemName)) return;

        items[itemName] -= quantity;

        if (items[itemName] <= 0)
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

        foreach (var pair in items)
        {
            inventoryText.text += "- " + pair.Key + " x" + pair.Value + "\n";
        }
    }
}