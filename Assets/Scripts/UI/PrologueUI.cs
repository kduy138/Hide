using UnityEngine;

public class PrologueUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.OnGamePlaying += GameManager_OnGamePlaying;
    }

    private void GameManager_OnGamePlaying(object sender, System.EventArgs e)
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
