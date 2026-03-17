using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    private bool collected = false;

    public GameObject winCanvas;

    void Start()
    {
        if (winCanvas != null)
            winCanvas.SetActive(false); // Hide canvas at start
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            if (winCanvas != null)
                winCanvas.SetActive(true); // Show Win Canvas

            Time.timeScale = 0f;
            gameObject.SetActive(false);

            Debug.Log("Congratulations! You collected the crystal and won the level!");
        }
    }
}