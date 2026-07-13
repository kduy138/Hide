using UnityEngine;

public class EnterDialogueScene : MonoBehaviour
{
    public void OnEnter()
    {
        if (ObjectiveManager.instance.GetCurrentState() == ObjectiveManager.State.TellYourFriends)
        {
            GameManager.instance.SetGameState(GameManager.State.Dialogue);

            Objective currentObjective = ObjectiveManager.instance.GetCurrentObjective();
            currentObjective.SetCompleted(true);
            currentObjective.SetIsActive(false);

            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
