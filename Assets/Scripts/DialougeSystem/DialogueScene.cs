using UnityEngine;

public class DialogueScene : MonoBehaviour
{
    public enum SceneIndex
    {
        Scene_1,
        Scene_2,
        Scene_3,
    }

    [SerializeField]
    private int sceneIdx;

    [Header("Dialogue Lines")]
    public DialogueLine[] dialogueLines;

    [Header("States")]
    private bool dialogueStarted = false;

    private void Start()
    {
        GameManager.instance.OnDialogue += GameManager_OnDialogue;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnDialogue -= GameManager_OnDialogue;
    }

    private void Update()
    {
        if (dialogueStarted && DialogueManager.instance.IsDialogueFinished())
        {
            dialogueStarted = false;
        }
    }

    private void GameManager_OnDialogue(object sender, System.EventArgs e)
    {
        if (sceneIdx != GameManager.instance.GetCurrentDialogueSceneIdx()) return;
        if (dialogueStarted) return;

        Debug.LogError("Scene Index: " + sceneIdx);
        Debug.LogError("Starting Dialogue");

        DialogueManager.instance.StartDialogue(dialogueLines);
        dialogueStarted = true;

        DialogueManager.instance.OnDialogueEnded += DialogueManager_OnDialogueEnded; ;
    }

    private void DialogueManager_OnDialogueEnded(object sender, System.EventArgs e)
    {
        DialogueManager.instance.OnDialogueEnded -= DialogueManager_OnDialogueEnded;
        GameManager.instance.AdvanceDialogueScene();
    }
}
