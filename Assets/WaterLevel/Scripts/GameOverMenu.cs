using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject menuCanvas;

    private bool menuOpen = false;

    void Start()
    {
        // Hide the menu when the game starts
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        menuOpen = !menuOpen;

        menuCanvas.SetActive(menuOpen);

        if (menuOpen)
            Time.timeScale = 0f; // pause
        else
            Time.timeScale = 1f; // resume
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ExitToMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMapLevel"); // placeholder name
    }
}