using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea(1, 4)]
    public string dialougeTxt;
}
