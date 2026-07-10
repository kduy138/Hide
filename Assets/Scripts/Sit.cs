using UnityEngine;

public class Sit : MonoBehaviour
{
    public void OnSit()
    {
        if (ObjectiveManager.instance.GetCurrentState() == ObjectiveManager.State.TellYourFriends)
        {
            GameManager.instance.SetGameState(GameManager.State.Dialogue);

            Objective currentObjective = ObjectiveManager.instance.GetCurrentObjective();
            currentObjective.SetCompleted(true);
            currentObjective.SetIsActive(false);
        }
    }
}
