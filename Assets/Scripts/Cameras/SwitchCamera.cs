using Cinemachine;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public static SwitchCamera instance { get; private set; }

    [Header("Cameras")]
    [SerializeField]
    private GameObject monopolyCam;
    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    private CinemachineVirtualCamera monopolyVirtualCam;
    [SerializeField]
    private CinemachineVirtualCamera windowsVirtualCam;
    [SerializeField]
    private CinemachineVirtualCamera talkToFriendVirtualCam;

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
        DialogueManager.instance.OnDialogueStarted += DialogueManager_OnDialogueStarted;
        DialogueManager.instance.OnDialogueEnded += DialogueManager_OnDialogueEnded;
    }

    private void DialogueManager_OnDialogueStarted(object sender, System.EventArgs e)
    {
        int currentDialogueSceneIdx = GameManager.instance.GetCurrentDialogueSceneIdx();
        switch (currentDialogueSceneIdx)
        {
            case (int)DialogueScene.SceneIndex.Scene_1:
                break;
            case (int)DialogueScene.SceneIndex.Scene_2:
                CinemachineCamSwitch(windowsVirtualCam);
                break;
            case (int)DialogueScene.SceneIndex.Scene_3:
                CinemachineCamSwitch(talkToFriendVirtualCam);
                break;
        }
    }

    private void DialogueManager_OnDialogueEnded(object sender, System.EventArgs e)
    {
        int currentDialogueSceneIdx = GameManager.instance.GetCurrentDialogueSceneIdx();
        switch (currentDialogueSceneIdx)
        {
            case (int)DialogueScene.SceneIndex.Scene_1:
                SwitchTo(mainCam, monopolyCam);
                break;
            case (int)DialogueScene.SceneIndex.Scene_2:
                //SwitchTo(mainCam, windowsCam);
                break;
            case (int)DialogueScene.SceneIndex.Scene_3:
                break;
        }
    }

    public void SwitchTo(GameObject targetCam, GameObject currentCam)
    {
        targetCam.SetActive(true);
        currentCam.SetActive(false);
    }

    public void CinemachineCamSwitch(CinemachineVirtualCamera targetCam)
    {
        SetAllPriority(0);
        targetCam.Priority = 10;
    }

    private void SetAllPriority(int value)
    {
        monopolyVirtualCam.Priority = value;
        windowsVirtualCam.Priority = value;
        talkToFriendVirtualCam.Priority = value;
    }
}
