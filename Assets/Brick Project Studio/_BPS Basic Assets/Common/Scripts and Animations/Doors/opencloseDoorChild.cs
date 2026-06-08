using SojaExiles;
using UnityEngine;

public class opencloseDoorChild : MonoBehaviour, IDoor
{
    [SerializeField]
    private opencloseDoor parentDoor;

    public void Interact()
    {
        parentDoor.Interact();
    }

    public bool IsOpen()
    {
        return parentDoor.IsOpen();
    }
}

