using UnityEngine;

public class PrologueUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.OnPrologue += GameManager_OnPrologue;

        Hide();
    }

    private void Update()
    {
        if (GameManager.instance.GetCurrentState() == GameManager.State.GamePlaying)
        {
            Hide();
        }
    }

    private void GameManager_OnPrologue(object sender, System.EventArgs e)
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
