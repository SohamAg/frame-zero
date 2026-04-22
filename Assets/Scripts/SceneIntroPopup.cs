using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SceneIntroPopup : MonoBehaviour
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
    public MonoBehaviour playerControllerToDisable;
    public MonoBehaviour otherSystemToDisable;

    [Header("Dialogue")]
    [TextArea(2, 5)]
    public string[] dialogueLines;

    public string speakerName = "Guide";

    [Header("Settings")]
    public bool playOnSceneStart = true;
    public bool disableOnlyOnce = false;

    private int currentLineIndex = 0;
    private bool introActive = false;
    private bool hasPlayed = false;

    private void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(ShowNextLine);

        if (skipButton != null)
            skipButton.onClick.AddListener(SkipIntro);

        if (playOnSceneStart)
        {
            if (!disableOnlyOnce || !hasPlayed)
            {
                StartIntro(dialogueLines);
            }
            else
            {
                EndIntroImmediate();
            }
        }
        else
        {
            EndIntroImmediate();
        }
    }

    private void Update()
    {
        if (!introActive) return;

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

    public void StartIntro(string[] newLines)
    {
        if (newLines != null && newLines.Length > 0)
        {
            dialogueLines = newLines;
        }

        introActive = true;
        currentLineIndex = 0;
        hasPlayed = true;

        if (introPanel != null)
            introPanel.SetActive(true);

        if (playerControllerToDisable != null)
            playerControllerToDisable.enabled = false;

        if (otherSystemToDisable != null)
            otherSystemToDisable.enabled = false;

        if (nameText != null)
            nameText.text = speakerName;

        ShowCurrentLine();
    }

    public void StartIntro()
    {
        StartIntro(dialogueLines);
    }

    private void ShowCurrentLine()
    {
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            EndIntro();
            return;
        }

        if (currentLineIndex < 0 || currentLineIndex >= dialogueLines.Length)
        {
            EndIntro();
            return;
        }

        if (dialogueText != null)
            dialogueText.text = dialogueLines[currentLineIndex];
    }

    public void ShowNextLine()
    {
        if (!introActive) return;

        currentLineIndex++;

        if (currentLineIndex >= dialogueLines.Length)
        {
            EndIntro();
            return;
        }

        ShowCurrentLine();
    }

    public void SkipIntro()
    {
        if (!introActive) return;

        EndIntro();
    }

    private void EndIntro()
    {
        EndIntroImmediate();
    }

    private void EndIntroImmediate()
    {
        introActive = false;

        if (introPanel != null)
            introPanel.SetActive(false);

        if (playerControllerToDisable != null)
            playerControllerToDisable.enabled = true;

        if (otherSystemToDisable != null)
            otherSystemToDisable.enabled = true;
    }
}