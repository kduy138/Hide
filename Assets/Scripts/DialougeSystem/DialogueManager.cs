using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public event EventHandler OnDialogueStarted;

    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI speakerNameTxt;
    [SerializeField]
    private TextMeshProUGUI dialogueTxt;

    [Header("Settings")]
    [SerializeField]
    private float dialogueTextSpeed = 0.05f;

    [Header("Status")]
    [SerializeField]
    private bool isTyping = false;
    [SerializeField]
    private bool isDialogueFinished = true;

    [Header("Dialogue")]
    [SerializeField]
    private DialogueLine[] dialogueLines;

    private Coroutine typingCoroutine;
    private int currentIndex = 0;
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

    private void Update()
    {
        
    }

    private void GameInput_OnDialogue(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void StartDialogue(DialogueLine[] newDialogueLines)
    {
        OnDialogueStarted?.Invoke(this, EventArgs.Empty);
        isDialogueFinished = false;
        dialogueLines = newDialogueLines;
        currentIndex = 0;
        justStarted = true;
        typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
    }

    private IEnumerator TypeLine(DialogueLine dialogueLine)
    {
        isTyping = true;

        dialogueTxt.text = "";
        speakerNameTxt.text = dialogueLine.speakerName;

        foreach (char c in dialogueLine.dialougeTxt)
        {
            dialogueTxt.text += c;
            yield return new WaitForSeconds(dialogueTextSpeed);
        }
        isTyping = false;
    }

    private void ShowEntireDialogue(DialogueLine dialogueLine)
    {
        dialogueTxt.text = dialogueLine.dialougeTxt;
        speakerNameTxt.text = dialogueLine.speakerName;
    }
}
