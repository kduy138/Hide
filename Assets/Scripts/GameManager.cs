using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnChallengeChanged;
    public event EventHandler OnHidingMinigameStarted;
    public event EventHandler OnGamePlaying;
    public event EventHandler OnPrologue;
    public event EventHandler OnDialogue;

    public enum State
    {
        NameInput,
        Prologue,
        Dialogue,
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
    private float waitingToStartHidingMinigameTimeMax = 5f;
    private float currentWaitingToStartHidingMinigameTime = 0f;
    private int currentDialogueSceneIdx = 0;

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

        state = State.NameInput;
        challenge = Challenge.None;
    }

    private void Update()
    {
        switch (state)
        {
            case State.NameInput:
                UnlockCursor();
                break;
            case State.Prologue:
                LockCursor();
                OnPrologue?.Invoke(this, EventArgs.Empty);
                break;
            case State.Dialogue:
                OnDialogue?.Invoke(this, EventArgs.Empty);
                break;
            case State.GamePlaying:
                Time.timeScale = 1f;
                OnGamePlaying?.Invoke(this, EventArgs.Empty);
                break;
            case State.GameOver:
                Time.timeScale = 0f;
                UnlockCursor();
                break;
        }
        //Debug.Log(state);

        switch (challenge)
        {
            case Challenge.None:
                currentWaitingToStartHidingMinigameTime = waitingToStartHidingMinigameTimeMax;
                break;
            case Challenge.Hide:
                currentWaitingToStartHidingMinigameTime -= Time.deltaTime;

                if (currentWaitingToStartHidingMinigameTime <= 0f)
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

    public void AdvanceDialogueScene()
    {
        currentDialogueSceneIdx++;
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public void SetGameState(State value)
    {
        if (state == value) return;

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

    public int GetCurrentDialogueSceneIdx()
    {
        return currentDialogueSceneIdx;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
