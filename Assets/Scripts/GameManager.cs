using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

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

    [Header("Stats")]
    private float gameTime = 68400f;

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

        //Debug.Log(challenge);
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
