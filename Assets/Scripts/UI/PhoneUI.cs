using UnityEngine;

public class PhoneUI : MonoBehaviour
{
    private void Start()
    {
        Player.instance.OnPlayerHasPhone += Player_OnPlayerHasPhone;
        Hide();
    }

    private void Player_OnPlayerHasPhone(object sender, System.EventArgs e)
    {
        Show();
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
