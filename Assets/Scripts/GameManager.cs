using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum State
    {
        GamePlaying,
        GameOver,
    }

    private State state;

    private void Awake()
    {
        instance = this;

        state = State.GamePlaying;
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
}
