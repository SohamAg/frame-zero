using UnityEngine;
using TMPro;

public class NPCTextTrigger : MonoBehaviour
{
    public GameObject canvasToDisplay;
    public TMP_Text questionText;

    private InventoryManager inventory;

    void Start()
    {
        if (canvasToDisplay != null)
            canvasToDisplay.SetActive(false);

        inventory = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToDisplay.SetActive(true);
            questionText.text = "Hello traveller! How can I be of assistance?";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToDisplay.SetActive(false);
        }
    }

    // OPTION 1
    public void SayHi()
    {
        questionText.text = "Well hello to you too! Safe travels.";
    }

    // OPTION 2
    public void LearnPowers()
    {
        questionText.text = "To obtain my powers, you must brew a potion using three rare items.";
    }

    // OPTION 3 — Give Items
    public void GiveItems()
    {
        if (inventory != null && inventory.GetItemCount("Fish") >= 3)
        {
            // Remove 3 fish
            inventory.RemoveItems("Fish", 3);

            // Give potion (could be just a message, or add a potion item)
            questionText.text = "Ah! You have everything I need. Here is your potion!";
            
            // Optional: Add Potion to inventory
            inventory.AddItem("Potion");
        }
        else
        {
            questionText.text = "You don’t have enough fish yet!";
        }
    }
}