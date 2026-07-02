using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }

    public event EventHandler OnDialogueStarted;
    public event EventHandler OnDialogueEnded;

    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI speakerNameTxt;
    [SerializeField]
    private TextMeshProUGUI dialogueTxt;
    [SerializeField]
    private string currentSpeakerName;

    [Header("Settings")]
    [SerializeField]
    private float dialogueTextSpeed = 0.1f;

    [Header("Status")]
    [SerializeField]
    private bool isTyping = false;
    [SerializeField]
    private bool isDialogueFinished = true;

    [Header("Dialogue")]
    [SerializeField]
    private DialogueLine[] dialogueLines;

    private Coroutine typingCoroutine;
    private int currentDialogueLineIndex = 0;
    private bool justStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameInput.instance.OnDialogue += GameInput_OnDialogue;
    }

    private void OnDestroy()
    {
        GameInput.instance.OnDialogue -= GameInput_OnDialogue;
    }

    private void GameInput_OnDialogue(object sender, EventArgs e)
    {
        if (isDialogueFinished) return;

        if (justStarted)
        {
            justStarted = false;
        }

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            ShowEntireDialogue(dialogueLines[currentDialogueLineIndex]);
            isTyping = false;
        }
        else
        {
            currentDialogueLineIndex++;

            if (currentDialogueLineIndex < dialogueLines.Length)
            {
                typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentDialogueLineIndex]));
            }
            else
            {
                dialogueTxt.text = "";
                speakerNameTxt.text = "";
                isDialogueFinished = true;
                GameManager.instance.SetGameState(GameManager.State.GamePlaying);
                OnDialogueEnded?.Invoke(this, EventArgs.Empty);
                SwitchCamera.instance.Switch();
            }
        }
    }

    public void StartDialogue(DialogueLine[] newDialogueLines)
    {
        if (newDialogueLines == null || newDialogueLines.Length == 0)
        {
            Debug.Log("No dialogue lines to start!");
            return;
        }

        //if (!isDialogueFinished) return;

        //if (typingCoroutine != null)
        //{
        //    StopCoroutine(typingCoroutine);
        //}

        OnDialogueStarted?.Invoke(this, EventArgs.Empty);
        isDialogueFinished = false;
        dialogueLines = newDialogueLines;
        currentDialogueLineIndex = 0;
        justStarted = true;
        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentDialogueLineIndex]));
    }

    private IEnumerator TypeLine(DialogueLine dialogueLine)
    {
        isTyping = true;

        dialogueTxt.text = "";
        speakerNameTxt.text = dialogueLine.GetSpeakerName();
        currentSpeakerName = dialogueLine.GetSpeakerName();

        string fullTxt = dialogueLine.GetDialogueText();

        foreach (char c in fullTxt)
        {
            dialogueTxt.text += c;
            yield return new WaitForSeconds(dialogueTextSpeed);
        }
        isTyping = false;
    }

    private void ShowEntireDialogue(DialogueLine dialogueLine)
    {
        dialogueTxt.text = dialogueLine.GetDialogueText();
        speakerNameTxt.text = dialogueLine.GetSpeakerName();
    }

    public bool IsDialogueFinished()
    {
        return isDialogueFinished;
    }

    public string GetCurrentSpeakerName()
    {
        return currentSpeakerName;
    }
}
