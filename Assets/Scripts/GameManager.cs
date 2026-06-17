using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnChallengeChanged;
    public event EventHandler OnHidingMinigameStarted;

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

    [Header("Settings")]
    private float gameTime = 68400f;
    private float waitingToStartTimeMax = 5f;
    private float currentWaitingToStartTime = 0f;

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

        state = State.GamePlaying;
        challenge = Challenge.None;
    }

    private void Update()
    {
        switch(state)
        {
            case State.GamePlaying:
                gameTime += Time.deltaTime;
                break;
            case State.GameOver:
                break;
        }

        switch(challenge)
        {
            case Challenge.None:
                if (Player.instance.IsInHidingSpot())
                {
                    challenge = Challenge.Hide;
                    currentWaitingToStartTime = waitingToStartTimeMax;
                    OnChallengeChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case Challenge.Hide:
                currentWaitingToStartTime -= Time.deltaTime;

                if (currentWaitingToStartTime <= 0f)
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
}
