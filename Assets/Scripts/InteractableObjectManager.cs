using System;
using UnityEngine;

public class InteractableObjectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject talkToYourFriendInte;

    private void Start()
    {
        DialogueManager.instance.OnDialogueStarted += DialogueManager_OnDialogueStarted;
        DialogueManager.instance.OnDialogueEnded += DialogueManager_OnDialogueEnded;
    }

    private void DialogueManager_OnDialogueStarted(object sender, EventArgs e)
    {
        int currentDialogueSceneIdx = GameManager.instance.GetCurrentDialogueSceneIdx();

        switch (currentDialogueSceneIdx)
        {
            case (int)DialogueScene.SceneIndex.Scene_1:
                break;
            case (int)DialogueScene.SceneIndex.Scene_2:
                
                break;
            case (int)DialogueScene.SceneIndex.Scene_3:
                talkToYourFriendInte.SetActive(false);
                break;
        }
    }

    private void DialogueManager_OnDialogueEnded(object sender, EventArgs e)
    {
        int currentDialogueSceneIdx = GameManager.instance.GetCurrentDialogueSceneIdx();

        switch (currentDialogueSceneIdx)
        {
            case (int)DialogueScene.SceneIndex.Scene_1:
                break;
            case (int)DialogueScene.SceneIndex.Scene_2:
                talkToYourFriendInte.SetActive(true);
                break;
            case (int)DialogueScene.SceneIndex.Scene_3:
                break;
        }
    }
}
