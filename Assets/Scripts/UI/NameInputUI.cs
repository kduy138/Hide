using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameInputField;
    [SerializeField]
    private Button confirmBtn;
    [SerializeField]
    private Button defaultNameBtn;

    private void Start()
    {
        confirmBtn.onClick.AddListener(() =>
        {
            OnConfirm();
        });

        defaultNameBtn.onClick.AddListener(() =>
        {
            OnDefaultName();
        });

        confirmBtn.interactable = false;
        nameInputField.onValueChanged.AddListener(OnInputChanged);

        nameInputField.Select();
        nameInputField.ActivateInputField();
    }

    private void OnInputChanged(string value)
    {
        confirmBtn.interactable = value.Trim().Length > 1;
    }

    private void OnConfirm()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName)) return;

        Player.instance.SetPlayerName(playerName);
        GameManager.instance.SetGameState(GameManager.State.Prologue);
        Hide();
    }

    private void OnDefaultName()
    {
        GameManager.instance.SetGameState(GameManager.State.Prologue);
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
