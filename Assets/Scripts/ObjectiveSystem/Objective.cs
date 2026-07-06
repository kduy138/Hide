using UnityEngine;

[System.Serializable]
public class Objective
{
    [SerializeField]
    private ObjectiveManager.State state;
    [SerializeField]
    private string objectiveDes;
    [SerializeField]
    private bool isActive;
    [SerializeField]
    private bool isCompleted;

    public string GetObjectiveDes()
    {
        return objectiveDes;
    }

    public ObjectiveManager.State GetState()
    {
        return state;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }

    public void SetIsActive(bool value)
    {
        if (isActive == value) return;

        isActive = value;
    }

    public void SetCompleted(bool value)
    {
        if (isCompleted == value) return;

        isCompleted = value;
    }
}
