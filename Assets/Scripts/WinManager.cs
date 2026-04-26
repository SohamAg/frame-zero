using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public string worldMapSceneName = "WorldMap";

    public GameObject winCanvas;

    void Start()
    {
        if (winCanvas != null)
            winCanvas.SetActive(false);
    }

    // Call this when player wins
    public void TriggerWin()
    {
        if (winCanvas != null)
            winCanvas.SetActive(true);

        // Optional: pause game
        Time.timeScale = 0f;
    }

    // Button function → return to world map
    public void ReturnToWorldMap()
    {
        Time.timeScale = 1f; // IMPORTANT: reset time

        SceneManager.LoadScene(worldMapSceneName);
    }

    // Optional: restart level
    public void RestartLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}