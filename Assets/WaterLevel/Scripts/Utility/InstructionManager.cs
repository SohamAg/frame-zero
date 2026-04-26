using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    public GameObject instructionsPanel;

    private MonsterAI[] monsters;
    private bool gameStarted = false;

    void Start()
    {
        instructionsPanel.SetActive(true);

        monsters = FindObjectsOfType<MonsterAI>();

        PauseGame(true);
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;

        instructionsPanel.SetActive(false);

        PauseGame(false);
    }

    void PauseGame(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;

        foreach (var m in monsters)
        {
            if (m != null)
                m.SetPaused(paused);
        }
    }
}