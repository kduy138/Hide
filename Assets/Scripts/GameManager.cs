using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnChallengeChanged;
    public event EventHandler OnHidingMinigameStarted;
    public event EventHandler OnPrologue;

    public enum State
    {
        Prologue,
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

    [Header("Settings")]
    private float gameTime = 68400f;
    private float waitingToStartMinigameTimeMax = 5f;
    private float currentWaitingToStartMinigameTime = 0f;

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

        state = State.Prologue;
        challenge = Challenge.None;
    }

    private void Update()
    {
        switch(state)
        {
            case State.Prologue:
                OnPrologue?.Invoke(this, EventArgs.Empty);
                break;
            case State.GamePlaying:
                Time.timeScale = 1f;
                gameTime += Time.deltaTime;

                break;
            case State.GameOver:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
        Debug.Log(state);

        switch(challenge)
        {
            case Challenge.None:
                currentWaitingToStartMinigameTime = waitingToStartMinigameTimeMax;
                break;
            case Challenge.Hide:
                currentWaitingToStartMinigameTime -= Time.deltaTime;

                if (currentWaitingToStartMinigameTime <= 0f)
                {
                    OnHidingMinigameStarted?.Invoke(this, EventArgs.Empty);
                    challenge = Challenge.OneMoreTime;
                    OnChallengeChanged?.Invoke(this, EventArgs.Empty);
                }
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

    public string GetFormattedTime()
    {
        int totalSeconds = Mathf.FloorToInt(gameTime);
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void SetGameState(State value)
    {
        state = value;
    }

    public State GetCurrentState()
    {
        return state;
    }

    public void SetGameChallenge(Challenge value)
    {
        challenge = value;
    }

    public Challenge GetCurrentChallenge()
    {
        return challenge;
    }
}
