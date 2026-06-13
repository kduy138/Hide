using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea(1, 4)]
    public string dialougeTxt;

    public string GetSpeakerName()
    {
        if (speakerName == "Player")
        {
            return Player.instance.GetPlayerName();
        }
        return speakerName;
    }
}
