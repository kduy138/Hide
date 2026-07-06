using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField]
    private GameObject container;

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (GameManager.instance.GetCurrentState() == GameManager.State.GamePlaying)
        {
            if (ObjectiveManager.instance.HasActiveObjective())
            {
                Show();
            }
        }
    }

    private void Hide()
    {
        container.SetActive(false);
    }

    private void Show()
    {
        container.SetActive(true);
    }
}
