using UnityEngine;

public class Billboarding : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private void Update()
    {
        Quaternion rotation = mainCamera.transform.rotation;
        transform.LookAt(worldPosition: transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
}
