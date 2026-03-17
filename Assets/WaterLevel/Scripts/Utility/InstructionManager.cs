using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    public GameObject instructionsPanel;

    private bool gameStarted = false;

    void Start()
    {
        if (instructionsPanel != null)
            instructionsPanel.SetActive(true); 
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

        if (instructionsPanel != null)
            instructionsPanel.SetActive(false); 
    }
}