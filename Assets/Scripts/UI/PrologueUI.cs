using UnityEngine;

public class PrologueUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.OnDialogue += GameManager_OnDialogue;
    }

    private void OnDisable()
    {
        GameManager.instance.OnDialogue -= GameManager_OnDialogue;
    }

    private void GameManager_OnDialogue(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
