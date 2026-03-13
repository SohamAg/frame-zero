using UnityEngine;
using TMPro;

public class NPCTextTrigger : MonoBehaviour
{
    public GameObject canvasToDisplay;
    public TMP_Text questionText;

    void Start()
    {
        canvasToDisplay.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToDisplay.SetActive(true);

            questionText.text =
            "Hello traveller! How can I be of assistance?";
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
        questionText.text =
        "To obtain my powers, you must brew a potion using three rare items. Find them and bring them to me.";
    }

    // OPTION 3
    public void GiveItems()
    {
        questionText.text =
        "Ah! You have everything I need. Let us begin the potion.";
    }
}