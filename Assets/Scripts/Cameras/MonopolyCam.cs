using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class MonopolyCam : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera monopolyCam;

    [SerializeField]
    private List<GameObject> characters;

    private void Update()
    {
        if (GameManager.instance.GetCurrentState() != GameManager.State.Dialogue) return;

        foreach (GameObject c in characters) {
            CharacterDialogueInfo cdi = c.GetComponent<CharacterDialogueInfo>();

            if (cdi.characterName == DialogueManager.instance.GetCurrentSpeakerName())
            {
                monopolyCam.LookAt = cdi.characterFocusPoint.transform;
            }
        }
    }    
}
