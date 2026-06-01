using SojaExiles;
using UnityEngine;

public class opencloseDoorChild : MonoBehaviour
{
    [SerializeField]
    private opencloseDoor parentDoor;

    private void OnMouseOver()
    {
        parentDoor.OnMouseOver();
    }
}

