using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput instance;

    private Player_InputActions playerInputActions;

    private void Awake()
    {
        instance = this;

        playerInputActions = new Player_InputActions();

        playerInputActions.Player.Enable();
    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
