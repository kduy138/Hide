using UnityEngine;

public class DialogueBoxUI : MonoBehaviour
{
    private void Start()
    {
        DialogueManager.instance.OnDialogueStarted += DialogueManager_OnDialogueStarted;
    }

    private void DialogueManager_OnDialogueStarted(object sender, System.EventArgs e)
    {
        Show();
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
