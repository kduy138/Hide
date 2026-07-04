using UnityEngine;

public class TV : MonoBehaviour, IEventObj
{
    [SerializeField]
    private GameObject tvScreen;

    private bool isTriggered = false;

    private void Start()
    {
        Hide();
    }

    public void TriggerEvent()
    {
        if (!isTriggered)
        {
            Show();
            isTriggered = true;
        }
    }

    private void Hide()
    {
        tvScreen.SetActive(false);
    }

    private void Show()
    {
        tvScreen.SetActive(true);
    }
}
