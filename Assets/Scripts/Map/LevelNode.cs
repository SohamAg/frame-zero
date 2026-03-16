using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelNode : MonoBehaviour
{
    public LevelType levelType;
    public string sceneToLoad;

    public Material lockedMaterial;
    public Material unlockedMaterial;
    public Material completedMaterial;

    private Renderer nodeRenderer;

    private bool playerNearby = false;

    private void Awake()
    {
        nodeRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        RefreshVisual();
    }

    private void Update()
    {
        if (playerNearby)
        {
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                TryEnterLevel();
            }
        }
    }

    public bool IsUnlocked()
    {
        switch (levelType)
        {
            case LevelType.Fire:
                return true;

            case LevelType.Earth:
                return GameProgress.fireCompleted;

            case LevelType.Water:
                return GameProgress.earthCompleted;

            case LevelType.Boss:
                return GameProgress.BossUnlocked();

            default:
                return false;
        }
    }

    public bool IsCompleted()
    {
        switch (levelType)
        {
            case LevelType.Fire:
                return GameProgress.fireCompleted;

            case LevelType.Earth:
                return GameProgress.earthCompleted;

            case LevelType.Water:
                return GameProgress.waterCompleted;

            case LevelType.Boss:
                return false;

            default:
                return false;
        }
    }

    public void RefreshVisual()
    {
        if (nodeRenderer == null) return;

        if (IsCompleted())
        {
            nodeRenderer.material = completedMaterial;
        }
        else if (IsUnlocked())
        {
            nodeRenderer.material = unlockedMaterial;
        }
        else
        {
            nodeRenderer.material = lockedMaterial;
        }
    }

    public void TryEnterLevel()
    {
        if (!IsUnlocked())
        {
            Debug.Log(levelType + " level is locked.");
            return;
        }

        Debug.Log("Loading scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player near " + levelType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}