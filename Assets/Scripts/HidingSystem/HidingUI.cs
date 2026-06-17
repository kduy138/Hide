using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HidingUI : MonoBehaviour
{
    [Header("Settings")]
    private float waitingToShowTimeMax = 2f;
    private float currentWaitingToShowTime = 0f;
    private bool isWaitingToShow = false;

    [Header("References")]
    [SerializeField]
    private GameObject hidingMinigameUI;
    [SerializeField]
    private GameObject hidingMinigameFailedUI;

    private void Start()
    {
        Player.instance.OnPlayerExitHidingSpot += Player_OnPlayerExitHidingSpot;
        HidingMinigame.instance.OnMinigameSuccess += HidingMinigame_OnMinigameSuccessOrFail;
        HidingMinigame.instance.OnMinigameFail += HidingMinigame_OnMinigameSuccessOrFail;
        GameManager.instance.OnHidingMinigameStarted += GameManager_OnMinigameStarted;

        HideMinigameUI();
    }

    private void Update()
    {
        if (!isWaitingToShow) return;

        currentWaitingToShowTime -= Time.deltaTime;

        if (currentWaitingToShowTime <= 0f)
        {
            isWaitingToShow = false;
            HidingMinigame.instance.ResumeMinigame();
            ShowMinigameUI();
        }
    }

    private void GameManager_OnMinigameStarted(object sender, System.EventArgs e)
    {
        ShowMinigameUI();
    }

    private void HidingMinigame_OnMinigameSuccessOrFail(object sender, System.EventArgs e)
    {
        HideMinigameUI();
        currentWaitingToShowTime = waitingToShowTimeMax;
        isWaitingToShow = true;
    }

    private void Player_OnPlayerExitHidingSpot(object sender, System.EventArgs e)
    {
        isWaitingToShow = false;
        HideMinigameUI();
    }

    private void ShowMinigameUI()
    {
        hidingMinigameUI.SetActive(true);
    }

    private void HideMinigameUI()
    {
        hidingMinigameUI.SetActive(false);
    }

    private void ShowMinigameFailedUI()
    {
        hidingMinigameFailedUI.SetActive(true);
    }

    private void HideMinigameFailedUI()
    {
        hidingMinigameFailedUI.SetActive(false);
    }
}
