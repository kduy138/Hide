using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [Header("Dialogue Lines")]
    [SerializeField]
    private DialogueLine[] dialogueLines;

    [Header("States")]
    private bool dialogueStarted = false;

    private void Start()
    {
        GameInput.instance.OnDialogue += GameInput_OnDialogue;
    }

    private void OnDestroy()
    {
        GameInput.instance.OnDialogue -= GameInput_OnDialogue;
    }

    private void Update()
    {
        if (dialogueStarted && DialogueManager.instance.IsDialogueFinished())
        {
            dialogueStarted = false;
        }
    }

    private void GameInput_OnDialogue(object sender, System.EventArgs e)
    {
        if (dialogueStarted) return;

        DialogueManager.instance.StartDialogue(dialogueLines);
        dialogueStarted = true;
    }
}
