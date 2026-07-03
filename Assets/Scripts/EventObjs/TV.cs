using UnityEngine;

public class TV : MonoBehaviour, IEventObj
{
    [SerializeField]
    private GameObject tvScreen;

    private void Start()
    {
        Hide();
    }

    public void TriggerEvent()
    {
        Show();
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
