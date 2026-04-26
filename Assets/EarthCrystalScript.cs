using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthCrystalScript : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;
        if (!other.CompareTag("Player")) return;

        isCollected = true;

        GameProgress.earthCompleted = true;
        GameProgress.earthCrystal = true;
        GameProgress.shieldUnlocked = true;

        SceneManager.LoadScene("WorldMap");
    }
}
