using UnityEngine;

public class HidingMinigame : MonoBehaviour
{
    public static HidingMinigame instance { get; private set; }

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
    private int currentRound = 1;
    private int failedCount = 0;

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

    private void OnEnable()
    {
        isMarkerStopped = false;
        SuccessZonePosRandomizer();
    }

    private void Update()
    {
        if (!Player.instance.IsInHidingSpot()) return;

        MoveMarker();

        if (failedCount >= 2)
        {
            Debug.Log("Game Over!");
        }

        if (currentRound >= maxRound)
        {
            Debug.Log("You Win!");
        }
    }

    private void GameInput_OnStopMarker(object sender, System.EventArgs e)
    {
        StopMarker();

        if (!isMarkerStopped) return;

        if (IsMarkerInSuccessZone())
        {
            currentRound++;
        }
        else
        {
            failedCount++;
        }
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

        float randomX = Random.Range(minX, maxX);

        successZoneRect.anchoredPosition = new Vector2(randomX, successZoneRect.anchoredPosition.y);
    }
}
