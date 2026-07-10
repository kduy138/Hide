using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance { get; private set; }

    public enum State
    {
        None,
        LookOutTheWindows,
        TellYourFriends,
        FindThePhone,
    }

    private State state;

    [SerializeField]
    private Objective[] objectives;

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

        state = State.None;
    }

    private void Start()
    {
        DialogueManager.instance.OnDialogueEnded += DialogueManager_OnDialogueEnded;
    }

    private void Update()
    {
        switch(state)
        {
            case State.None:
                break;
            case State.LookOutTheWindows:
                break;
            case State.FindThePhone:
                break;
        }
    }

    private void DialogueManager_OnDialogueEnded(object sender, System.EventArgs e)
    {
        int currentDialogueSceneIdx = GameManager.instance.GetCurrentDialogueSceneIdx();
        switch(currentDialogueSceneIdx) {
            case (int)DialogueScene.SceneIndex.Scene_1:
                SetCurrentObjective(State.LookOutTheWindows);
                GetCurrentObjective().SetIsActive(true);
                break;
            case (int)DialogueScene.SceneIndex.Scene_2:
                SetCurrentObjective(State.TellYourFriends);
                GetCurrentObjective().SetIsActive(true);
                break;
        }
    }

    public Objective GetCurrentObjective()
    {
        Objective currentObjective = System.Array.Find(objectives, o => o.GetState() == GetCurrentState());
        return currentObjective;
    }

    public State GetCurrentState()
    {
        return state;
    }

    public void SetCurrentObjective(State value)
    {
        if (state == value) return;

        state = value;

        Objective currentObjective = System.Array.Find(objectives, o => o.GetState() == value);
        if (currentObjective != null)
        {
            currentObjective.SetIsActive(true);
        }
    } 

    public bool HasActiveObjective()
    {
        foreach (Objective o in  objectives)
        {
            if (o.IsActive()) return true;
        }
        return false;
    }
}
