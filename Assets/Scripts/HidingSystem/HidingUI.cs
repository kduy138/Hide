using UnityEngine;

public class HidingUI : MonoBehaviour
{
    private void Start()
    {
        Player.instance.OnPlayerEnterHidingSpot += Player_OnPlayerInHidingSpot;
        Player.instance.OnPlayerExitHidingSpot += Player_OnPlayerExitHidingSpot;

        Hide();
    }

    private void Player_OnPlayerInHidingSpot(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Player_OnPlayerExitHidingSpot(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
