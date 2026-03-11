using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelCompleteTester : MonoBehaviour
{
    public LevelType levelType;

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.lKey.wasPressedThisFrame)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        switch (levelType)
        {
            case LevelType.Fire:
                GameProgress.fireCompleted = true;
                GameProgress.fireCrystal = true;
                GameProgress.swordUnlocked = true;
                break;

            case LevelType.Earth:
                GameProgress.earthCompleted = true;
                GameProgress.earthCrystal = true;
                GameProgress.shieldUnlocked = true;
                break;

            case LevelType.Water:
                GameProgress.waterCompleted = true;
                GameProgress.waterCrystal = true;
                GameProgress.spellUnlocked = true;
                break;

            case LevelType.Boss:
                Debug.Log("Boss level complete.");
                break;
        }

        Debug.Log("Level Complete: " + levelType);
        SceneManager.LoadScene("WorldMap");
    }
}