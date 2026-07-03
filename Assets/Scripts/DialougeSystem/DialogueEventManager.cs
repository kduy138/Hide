using UnityEngine;

public class DialogueEventManager : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.instance.GetCurrentState() != GameManager.State.Dialogue) return;

        DialogueEvent currentDialogueEvent = DialogueManager.instance.GetCurrentDialogueEvent();

        if (currentDialogueEvent.hasDialogueEvent && currentDialogueEvent.eventObj != null)
        {
            var eventObject = currentDialogueEvent.eventObj.GetComponent<IEventObj>();
            eventObject.TriggerEvent();
        }
    }
}
