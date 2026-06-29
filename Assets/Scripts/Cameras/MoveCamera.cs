using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform cameraPos;

    [Header("Settings")]
    private float playerStandHeight = 0f;
    private float playerCrouchHeight = 0.8f;
    private float crouchSpeed = 5f;

    private void Update()
    {
        float targetHeight = Player.instance.IsCrouching() ? playerCrouchHeight : playerStandHeight;

        Vector3 targetPos = cameraPos.position - Vector3.up * targetHeight;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * crouchSpeed);
    }
}
