using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string speakerName;
    [SerializeField]
    [TextArea(1, 4)]
    private string dialougeTxt;
    [SerializeField]
    private CameraEvent cameraEvent;
    [SerializeField]
    private DialogueEvent dialogueEvent;

    public string GetSpeakerName()
    {
        if (speakerName == "<player>")
        {
            return Player.instance.GetPlayerName();
        }
        return speakerName;
    }

    public string GetDialogueText()
    {
        return dialougeTxt.Replace("<player>", Player.instance.GetPlayerName());
    }

    public int GetDialogueID()
    {
        return id;
    }

    public CameraEvent GetCameraEvent()
    {
        return cameraEvent;
    }

    public DialogueEvent GetDialogueEvent() 
    {
        return dialogueEvent;
    }
}
