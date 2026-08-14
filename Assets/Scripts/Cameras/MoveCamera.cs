using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform cameraPos;

    [Header("Settings")]
    private float playerStandHeight = 0f;
    [SerializeField]
    private float playerCrouchHeight = 0.8f;
    [SerializeField]
    private float crouchSpeed = 5f;
    private float currentHeightOffset = 0f;

    private void Update()
    {
        float targetHeightoFfset = Player.instance.IsCrouching() ? playerCrouchHeight : playerStandHeight;

        currentHeightOffset = Mathf.Lerp(currentHeightOffset, targetHeightoFfset, Time.deltaTime * crouchSpeed);

        transform.position = cameraPos.position - Vector3.up * currentHeightOffset;
    }
}
