using SojaExiles;
using UnityEngine;

public class ClosetDoorChild : MonoBehaviour, IDoor
{
    private ClosetopencloseDoor parentDoor;

    private void Awake()
    {
        parentDoor = GetComponentInParent<ClosetopencloseDoor>();
    }

    public void Interact()
    {
        parentDoor.Interact();
    }

    public bool IsOpen()
    {
        return parentDoor.IsOpen();
    }
}
