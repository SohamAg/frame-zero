using UnityEngine;
using TMPro;

public class NPCTextTrigger : MonoBehaviour
{
    public GameObject canvasToDisplay;
    public TMP_Text questionText;

    private InventoryManager inventory;

    // Crystal spawning
    public GameObject crystalPrefab;
    public Transform crystalSpawnPoint;

    // Win Canvas
    private GameObject winCanvas;

    void Start()
    {
        if (canvasToDisplay != null)
            canvasToDisplay.SetActive(false);

        inventory = FindObjectOfType<InventoryManager>();

        // Find WinCanvas in the scene
        winCanvas = GameObject.Find("WinCanvas");
        if (winCanvas != null)
            winCanvas.SetActive(false); // Hide it at start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToDisplay.SetActive(true);
            questionText.text = "Wizard: Hello traveller! How can I be of assistance?";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToDisplay.SetActive(false);
        }
    }

    // hello
    public void SayHi()
    {
        questionText.text = "Wizard: Well hello to you too! Safe travels.";
    }

    // teach
    public void LearnPowers()
    {
        questionText.text = "Wizard: To obtain my powers, you must brew a potion using six items. Bring me 3 fish and 3 crystal shards";
    }

    // give
    public void GiveItems()
    {
        if (inventory != null && inventory.GetItemCount("Fish") >= 3 && inventory.GetItemCount("Shards") >= 3)
        {
            // Remove 3 fish
            inventory.RemoveItems("Fish", 3);
            inventory.RemoveItems("Shards", 3);

            // Give potion
            questionText.text = "Wizard: Ah! You have everything I need. Here is your potion!";

            inventory.AddItem("Potion");

            // Spawn crystal at spawn point
            if (crystalPrefab != null && crystalSpawnPoint != null)
            {
                GameObject crystal = Instantiate(crystalPrefab, crystalSpawnPoint.position, Quaternion.identity);

                CrystalPickup cp = crystal.GetComponent<CrystalPickup>();
                if (cp != null)
                {
                    cp.winCanvas = winCanvas;
                }
            }
        }
        else
        {
            questionText.text = "Wizard: You don’t have enough fish yet!";
        }
    }
}