using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Transform orientation;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        Vector2 inputVector = GameInput.instance.GetInputVectorNormalized();

        float horizontalInput = inputVector.x;
        float verticalInput = inputVector.y;

        float moveDistance = moveSpeed * Time.deltaTime;
        Vector3 moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        transform.position += moveDir * moveDistance;
    }
}
