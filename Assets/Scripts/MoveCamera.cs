using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform cameraPos;

    private void Update()
    {
        transform.position = cameraPos.position;
    }
}
