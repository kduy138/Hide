using Cinemachine;
using UnityEngine;

public class SceneCam : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera sceneCam;

    private void Update()
    {
        if (GameManager.instance.GetCurrentState() != GameManager.State.Dialogue) return;

        CameraEvent currentCameraEvent = DialogueManager.instance.GetCurrentCameraEvent();

        if (currentCameraEvent.hasCameraEvent && currentCameraEvent.lookAtTarget != null)
        {
            CharacterDialogueInfo cdi  = currentCameraEvent.lookAtTarget.GetComponent<CharacterDialogueInfo>();
            sceneCam.LookAt = cdi.characterFocusPoint.transform;
        }
    }    
}
