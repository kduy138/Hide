using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event EventHandler OnStateChanged;
    public event EventHandler OnChallengeChanged;

    public enum State
    {
        GamePlaying,
        GameOver,
    }

    public enum Challenge
    {
        None,
        Hide,
        OneMoreTime,
        BreakTime,
        FindThePhone,
        CloseYourEyes,
        PointToSacrifice,
    }

    private State state;
    private Challenge challenge;

    private void Awake()
    {
        instance = this;

        state = State.GamePlaying;
        challenge = Challenge.None;
    }

    private void Update()
    {
        switch(state)
        {
            case State.GamePlaying:
                break;
            case State.GameOver:
                break;
        }

        switch(challenge)
        {
            case Challenge.None:
                challenge = Challenge.Hide;
                OnChallengeChanged?.Invoke(this, EventArgs.Empty);
                break;
            case Challenge.Hide:
                break;
            case Challenge.OneMoreTime:
                break;
            case Challenge.BreakTime:
                break;
            case Challenge.FindThePhone:
                break;
            case Challenge.CloseYourEyes:
                break;
            case Challenge.PointToSacrifice:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
}
