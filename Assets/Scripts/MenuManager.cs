using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadWorldMap()
    {
        StartCoroutine(LoadWorldMapAfterDelay());
    }

    private IEnumerator LoadWorldMapAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("WorldMap");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
