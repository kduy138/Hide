using UnityEngine;

public class DialogueBoxUI : MonoBehaviour
{
    private void Start()
    {
        DialogueManager.instance.OnDialogueStarted += DialogueManager_OnDialogueStarted;
        DialogueManager.instance.OnDialogueEnded += DialogueManager_OnDialogueEnded;

        //Hide();
    }

    private void OnDisable()
    {
        DialogueManager.instance.OnDialogueStarted -= DialogueManager_OnDialogueStarted;
        DialogueManager.instance.OnDialogueEnded -= DialogueManager_OnDialogueEnded;
    }

    private void DialogueManager_OnDialogueStarted(object sender, System.EventArgs e)
    {
        Show();
    }
    private void DialogueManager_OnDialogueEnded(object sender, System.EventArgs e)
    {
        Hide();
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
