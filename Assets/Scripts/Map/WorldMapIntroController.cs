using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WorldMapIntroController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject introPanel;
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public Image characterImage;

    [Header("Buttons")]
    public Button nextButton;
    public Button skipButton;

    [Header("Optional References")]
    public MonoBehaviour mapPlayerController;
    public MonoBehaviour worldMapManager;

    [Header("Audio")]
    public AudioSource uiAudioSource;
    public float endDelay = 0.2f;

    [Header("Dialogue")]
    [TextArea(2, 5)]
    public string[] dialogueLines;

    public string speakerName = "Guide";

    private int currentLineIndex = 0;
    private bool introActive = false;
    private bool endingIntro = false;

    private void Start()
    {
        if (!GameProgress.worldMapIntroPlayed)
        {
            StartIntro();
        }
        else
        {
            EndIntroImmediate();
        }

        if (nextButton != null)
            nextButton.onClick.AddListener(ShowNextLine);

        if (skipButton != null)
            skipButton.onClick.AddListener(SkipIntro);
    }

    private void Update()
    {
        if (!introActive || endingIntro) return;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
            {
                ShowNextLine();
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                SkipIntro();
            }
        }
    }

    public void StartIntro()
    {
        introActive = true;
        endingIntro = false;
        currentLineIndex = 0;

        if (introPanel != null)
            introPanel.SetActive(true);

        if (mapPlayerController != null)
            mapPlayerController.enabled = false;

        if (worldMapManager != null)
            worldMapManager.enabled = false;

        if (nameText != null)
            nameText.text = speakerName;

        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            StartCoroutine(EndIntroAfterDelay());
            return;
        }

        if (currentLineIndex < 0 || currentLineIndex >= dialogueLines.Length)
        {
            StartCoroutine(EndIntroAfterDelay());
            return;
        }

        if (dialogueText != null)
            dialogueText.text = dialogueLines[currentLineIndex];
    }

    public void ShowNextLine()
    {
        if (!introActive || endingIntro) return;

        PlayUISound();

        currentLineIndex++;

        if (currentLineIndex >= dialogueLines.Length)
        {
            StartCoroutine(EndIntroAfterDelay());
            return;
        }

        ShowCurrentLine();
    }

    public void SkipIntro()
    {
        if (!introActive || endingIntro) return;

        PlayUISound();
        StartCoroutine(EndIntroAfterDelay());
    }

    private void PlayUISound()
    {
        if (uiAudioSource != null)
        {
            uiAudioSource.Play();
        }
    }

    private IEnumerator EndIntroAfterDelay()
    {
        endingIntro = true;

        yield return new WaitForSeconds(endDelay);

        EndIntro();
    }

    private void EndIntro()
    {
        GameProgress.worldMapIntroPlayed = true;
        EndIntroImmediate();
    }

    private void EndIntroImmediate()
    {
        introActive = false;
        endingIntro = false;

        if (introPanel != null)
            introPanel.SetActive(false);

        if (mapPlayerController != null)
            mapPlayerController.enabled = true;

        if (worldMapManager != null)
            worldMapManager.enabled = true;
    }
}