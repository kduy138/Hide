using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public event EventHandler OnPlayerHasPhone;

    [Header("References")]
    [SerializeField]
    private Transform orientation;
    private Rigidbody rb;
    [SerializeField]
    private LayerMask layerMask;

    [Header("Settings")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float crouchMoveSpeed;
    [SerializeField]
    private float interactDistance;

    [Header("Flags")]
    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private bool isCrouching = false;
    [SerializeField]
    private bool isHiding = false;
    [SerializeField]
    private bool hasPhone = false;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Start()
    {
        GameInput.instance.OnCrouchAction += GameInput_OnCrouchAction;
        GameInput.instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update()
    {
        Debug.Log("Player is hinding: " + isHiding);
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void GameInput_OnCrouchAction(object sender, System.EventArgs e)
    {
        isCrouching = !isCrouching;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        Debug.Log("Interacted!");
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = GameInput.instance.GetInputVectorNormalized();

        float horizontalInput = inputVector.x;
        float verticalInput = inputVector.y;

        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit hit, interactDistance))
        {
            if (hit.transform.TryGetComponent(out Phone phone)) {
                Debug.Log("Hit phone: " + hit.transform.gameObject.name);
                phone.Interact();
                OnPlayerHasPhone?.Invoke(this, EventArgs.Empty);
            }
        }
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

        bool canMove = !Physics.CapsuleCast(capsulePoint1, capsulePoint2, playerRadius, moveDir, moveDistance, layerMask);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f);
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(capsulePoint1, capsulePoint2, playerRadius, moveDirX, moveDistance, layerMask);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z);
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(capsulePoint1, capsulePoint2, playerRadius, moveDirZ, moveDistance, layerMask);

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

        isMoving = moveDir != Vector3.zero;
    }

    public void SetPlayerHasPhone(bool value)
    {
        hasPhone = value;
    }

    public void SetPlayerIsHiding(bool value)
    {
        isHiding = value;
    }

    public bool HasPhone()
    {
        return hasPhone;
    }

    public bool IsWalking()
    {
        return isMoving;
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }

    public bool IsHiding()
    {
        return isHiding;
    }
}
