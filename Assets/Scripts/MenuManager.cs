using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadWorldMap()
    {
        GameProgress.ResetProgress();
        SceneManager.LoadScene("WorldMap");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
