using SojaExiles;
using UnityEngine;

public class ClosetDoorChild : MonoBehaviour
{
    private ClosetopencloseDoor parentDoor;

    private void Awake()
    {
        parentDoor = GetComponentInParent<ClosetopencloseDoor>();
    }

    private void OnMouseOver()
    {
        parentDoor.OnMouseOver();
    }
}
