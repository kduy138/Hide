using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
    private TextMeshProUGUI currentObjectiveNameTxt;
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
            currentObjectiveNameTxt.text = currentObjective.GetObjectiveDes();
            currentObjective.SetIsActive(true);
        }
        else
        {
            currentObjectiveNameTxt.text = string.Empty;
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
