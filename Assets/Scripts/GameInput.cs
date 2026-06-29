using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }

    public event EventHandler OnCrouchAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnOpenClose;
    public event EventHandler OnDialogue;
    public event EventHandler OnPrologue;
    public event EventHandler OnStopMarker;

    [Description("Test events")]
    public event EventHandler OnSwitchCamera;

    private Player_InputActions playerInputActions;

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

        playerInputActions = new Player_InputActions();

        playerInputActions.Player.Enable();

        playerInputActions.Player.Crouch.performed += Crouch_performed;
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.OpenClose.performed += OpenClose_performed;
        playerInputActions.Player.Dialogue.performed += Dialogue_performed;
        playerInputActions.Player.Dialogue.performed += Prologue_performed;
        playerInputActions.Player.StopMarker.performed += StopMarker_performed;

        playerInputActions.Test.SwitchCamera.performed += SwitchCamera_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Crouch.performed -= Crouch_performed;
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.OpenClose.performed -= OpenClose_performed;
        playerInputActions.Player.Dialogue.performed -= Dialogue_performed;
        playerInputActions.Player.Dialogue.performed -= Prologue_performed;
        playerInputActions.Player.StopMarker.performed -= StopMarker_performed;

        playerInputActions.Test.SwitchCamera.performed -= SwitchCamera_performed;

        playerInputActions.Dispose();
    }

    private void SwitchCamera_performed(InputAction.CallbackContext obj)
    {
        OnSwitchCamera?.Invoke(this, EventArgs.Empty);
    }

    private void Prologue_performed(InputAction.CallbackContext obj)
    {
        OnPrologue?.Invoke(this, EventArgs.Empty);
    }

    private void StopMarker_performed(InputAction.CallbackContext obj)
    {
        OnStopMarker?.Invoke(this, EventArgs.Empty);
    }

    private void Dialogue_performed(InputAction.CallbackContext obj)
    {
        OnDialogue?.Invoke(this, EventArgs.Empty);
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
