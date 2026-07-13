using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }

    public event EventHandler OnPlayerHasPhone;
    public event EventHandler OnPlayerEnterHidingSpot;
    public event EventHandler OnPlayerExitHidingSpot;

    [Header("References")]
    [SerializeField]
    private Transform orientation;
    private Rigidbody rb;
    [SerializeField]
    private LayerMask layerMask;

    [Header("Settings")]
    [SerializeField]
    private string playerName;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float crouchMoveSpeed;
    [SerializeField]
    private float doorInteractDistance;
    [SerializeField]
    private float phoneInteractDistance;

    [Header("Flags")]
    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private bool isCrouching = false;
    [SerializeField]
    private bool canBeDetected = true;
    [SerializeField]
    private bool inHidingSpot = false;
    [SerializeField]
    private bool hasPhone = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Start()
    {
        GameInput.instance.OnCrouchAction += GameInput_OnCrouchAction;
        GameInput.instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.instance.OnOpenClose += GameInput_OnOpenClose;
    }

    private void OnDestroy()
    {
        GameInput.instance.OnCrouchAction -= GameInput_OnCrouchAction;
        GameInput.instance.OnInteractAction -= GameInput_OnInteractAction;
        GameInput.instance.OnOpenClose -= GameInput_OnOpenClose;
    }

    private void Update()
    {
        if (GameManager.instance.GetCurrentState() != GameManager.State.GamePlaying) return;

        if (inHidingSpot)
        {
            canBeDetected = isMoving ? true : false;
        }
        else
        {
            canBeDetected = true;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.GetCurrentState() != GameManager.State.GamePlaying) return;

        HandlePlayerMovement();
    }

    private void GameInput_OnCrouchAction(object sender, System.EventArgs e)
    {
        isCrouching = !isCrouching;
    }

    private void GameInput_OnOpenClose(object sender, EventArgs e)
    {
        HandlePlayerDoorInteraction();
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (GameManager.instance.GetCurrentState() != GameManager.State.GamePlaying) return;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, phoneInteractDistance))
        {
            if (hit.transform.TryGetComponent(out Phone phone)) {
                phone.Interact();
                OnPlayerHasPhone?.Invoke(this, EventArgs.Empty);
            }
            if (hit.transform.TryGetComponent(out EnterDialogueScene enter))
            {
                enter.OnEnter();
            }
        }

        
    }

    private void HandlePlayerDoorInteraction()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitDoor, doorInteractDistance))
        {
            if (hitDoor.transform.TryGetComponent(out IDoor door))
            {
                door.Interact();
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

    public void SetPlayerInHidingSpot(bool value)
    {
        if (inHidingSpot == value) return;

        inHidingSpot = value;

        if (value == true)
        {
            OnPlayerEnterHidingSpot?.Invoke(this, EventArgs.Empty);
            GameManager.instance.SetGameChallenge(GameManager.Challenge.Hide);
        }
        else
        {
            OnPlayerExitHidingSpot?.Invoke(this, EventArgs.Empty);
        }
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

    public bool IsInHidingSpot()
    {
        return inHidingSpot;
    }

    public bool CanBeDetected()
    {
        return canBeDetected;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string value)
    {
        playerName = value;
    }
}
