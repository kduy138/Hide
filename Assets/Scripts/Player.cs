using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("References")]
    [SerializeField]
    private Transform orientation;
    private Rigidbody rb;

    [Header("Settings")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float crouchMoveSpeed;
    private bool isWalking = false;
    private bool isCrouching = false;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Start()
    {
        GameInput.instance.OnCrouchAction += GameInput_OnCrouchAction;
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void GameInput_OnCrouchAction(object sender, System.EventArgs e)
    {
        isCrouching = !isCrouching;
    }

    private void HandlePlayerMovement()
    {
        Vector2 inputVector = GameInput.instance.GetInputVectorNormalized();

        float playerRadius = 0.5f;
        float playerHeight = 2f;

        float horizontalInput = inputVector.x;
        float verticalInput = inputVector.y;

        float moveDistance;
        if (!isCrouching)
        {
            moveDistance = moveSpeed * Time.deltaTime;
        }
        else
        {
            moveDistance = crouchMoveSpeed * Time.deltaTime;
        }

        Vector3 moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        Vector3 capsulePoint1 = transform.position - Vector3.up * (playerHeight / 2 - playerRadius);
        Vector3 capsulePoint2 = transform.position + Vector3.up * (playerHeight / 2 - playerRadius);

        bool canMove = !Physics.CapsuleCast(capsulePoint1, capsulePoint2, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f);
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(capsulePoint1, capsulePoint2, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z);
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(capsulePoint1, capsulePoint2, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {

                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }
}
