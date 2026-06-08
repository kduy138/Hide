using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance;

    public event EventHandler OnCrouchAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnOpenClose;

    private Player_InputActions playerInputActions;

    private void Awake()
    {
        instance = this;

        playerInputActions = new Player_InputActions();

        playerInputActions.Player.Enable();

        playerInputActions.Player.Crouch.performed += Crouch_performed;
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.OpenClose.performed += OpenClose_performed;
    }


    private void OnDestroy()
    {
        playerInputActions.Player.Crouch.performed -= Crouch_performed;
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.OpenClose.performed -= OpenClose_performed;

        playerInputActions.Dispose();
    }

    private void OpenClose_performed(InputAction.CallbackContext obj)
    {
        OnOpenClose?.Invoke(this, EventArgs.Empty);
    }

    private void Crouch_performed(InputAction.CallbackContext context)
    {
        OnCrouchAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
