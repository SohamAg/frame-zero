using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Call this for the Restart button
    public void RestartLevel()
    {
        Time.timeScale = 1f; // reset time scale
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Call this for the Exit to Map button
    public void ExitToMap()
    {
        Time.timeScale = 1f; // reset time scale
        SceneManager.LoadScene("MapLevel"); // Replace with your actual map scene name
    }
}