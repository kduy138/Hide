using System;
using UnityEngine;

public class HidingMinigame : MonoBehaviour
{
    public static HidingMinigame instance { get; private set; }

    public event EventHandler OnMinigameSuccess;
    public event EventHandler OnMinigameFail;
    public event EventHandler OnMinigameLose;

    [Header("References")]
    [SerializeField]
    private GameObject trackArea;
    [SerializeField]
    private GameObject successZone;
    [SerializeField]
    private GameObject marker;

    [Header("Settings")]
    [SerializeField]
    private float markerSpeed;
    private float trackAreaWidth = 500f;
    private float successZoneWidth = 70f;
    private int maxRound = 5;
    [SerializeField]
    private int currentRound = 0;
    [SerializeField]
    private int failedCount = 0;
    private float minigameDuration = 5f;
    private float currentMinigameTime = 0f;
    private float timeBarFillAmount = 0f;

    [Header("States")]
    private bool isMarkerStopped;

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
    }

    private void Start()
    {
        GameInput.instance.OnStopMarker += GameInput_OnStopMarker;
    }

    private void Update()
    {
        if (GameManager.instance.GetCurrentState() == GameManager.State.GameOver) return;
        if (!Player.instance.IsInHidingSpot()) return;

        MoveMarker();

        if (currentMinigameTime <= 0f) return;

        currentMinigameTime -= Time.deltaTime;
        timeBarFillAmount = currentMinigameTime / minigameDuration;

        if (currentMinigameTime <= 0f)
        {
            currentMinigameTime = 0f;
        }
    }

    private void GameInput_OnStopMarker(object sender, System.EventArgs e)
    {
        if (!Player.instance.IsInHidingSpot()) return;
        if (isMarkerStopped) return;

        StopMarker();

        if (IsMarkerInSuccessZone())
        {
            currentRound++;
            OnMinigameSuccess?.Invoke(this, EventArgs.Empty);

            if (currentRound >= maxRound && failedCount < 2)
            {
                Debug.Log("You Win!");
            }
        }
        else
        {
            failedCount++;
            currentRound++;
            OnMinigameFail?.Invoke(this, EventArgs.Empty);

            if (failedCount >= 2 && currentRound <= maxRound)
            {
                GameManager.instance.SetGameState(GameManager.State.GameOver);
                OnMinigameLose?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void ResumeMinigame()
    {
        isMarkerStopped = false;
        StartTimer();
        SuccessZonePosRandomizer();
    }

    public void StartTimer()
    {
        currentMinigameTime = minigameDuration;
    }

    private void MoveMarker()
    {
        if (isMarkerStopped) return;

        if (marker == null)
        {
            Debug.LogError("Missing a game object reference for Marker!");
            return;
        }

        float posX = Mathf.PingPong(Time.time * markerSpeed * trackAreaWidth, trackAreaWidth);

        RectTransform markerRect = marker.GetComponent<RectTransform>();
        markerRect.anchoredPosition = new Vector2(posX - trackAreaWidth / 2f, markerRect.anchoredPosition.y);
    }

    private void StopMarker()
    {
        if (!Player.instance.IsInHidingSpot()) return;

        if (marker == null)
        {
            Debug.LogError("Missing a game object reference for Marker!");
            return;
        }

        isMarkerStopped = true;
    }

    private bool IsMarkerInSuccessZone()
    {
        if (marker == null || successZone == null)
        {
            Debug.LogError("Missing game object references!");
            return false;
        }

        RectTransform markerRect = marker.GetComponent<RectTransform>();
        RectTransform successZoneRect = successZone.GetComponent<RectTransform>();

        float successZoneLeft = successZoneRect.anchoredPosition.x - successZoneWidth / 2f;
        float successZoneRight = successZoneRect.anchoredPosition.x + successZoneWidth / 2f;

        if (markerRect.anchoredPosition.x >= successZoneLeft && markerRect.anchoredPosition.x <= successZoneRight)
        {
            return true;
        }

        return false;
    }

    private void SuccessZonePosRandomizer()
    {
        if (successZone == null || trackArea == null)
        {
            Debug.LogError("Missing game object references!");
            return;
        }

        RectTransform successZoneRect = successZone.GetComponent<RectTransform>();

        float minX = -trackAreaWidth / 2f + successZoneWidth / 2f;
        float maxX = trackAreaWidth / 2f - successZoneWidth / 2f;

        float randomX = UnityEngine.Random.Range(minX, maxX);

        successZoneRect.anchoredPosition = new Vector2(randomX, successZoneRect.anchoredPosition.y);
    }

    public float TimeBarFillAmount()
    {
        return timeBarFillAmount;
    }

    public float GetCurrentMinigameTime()
    {
        return currentMinigameTime;
    }
}
