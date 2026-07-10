using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private TextMeshProUGUI currentObjectiveNameTxt;

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (GameManager.instance.GetCurrentState() == GameManager.State.GamePlaying)
        {
            if (!ObjectiveManager.instance.HasActiveObjective())
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
        else
        {
            Hide();
        }

        Objective currentObjective = ObjectiveManager.instance.GetCurrentObjective();
        if (currentObjective != null)
        {
            currentObjectiveNameTxt.text = currentObjective.GetObjectiveDes();
        }
        else
        {
            currentObjectiveNameTxt.text = string.Empty;
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
