using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea(1, 4)]
    public string dialougeTxt;

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
}
