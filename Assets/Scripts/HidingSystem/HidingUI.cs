using System.Collections;
using UnityEngine;

public class HidingUI : MonoBehaviour
{
    [Header("Settings")]
    private float waitingTimeMax = 2f;
    private float currentWaitingTime = 0f;
    private bool isWaitingToShow = false;

    [Header("References")]
    [SerializeField]
    private GameObject hidingMinigameUI;

    private void Start()
    {
        Player.instance.OnPlayerEnterHidingSpot += Player_OnPlayerInHidingSpot;
        Player.instance.OnPlayerExitHidingSpot += Player_OnPlayerExitHidingSpot;
        HidingMinigame.instance.OnMinigameSuccess += HidingMinigame_OnMinigameSuccessOrFail;
        HidingMinigame.instance.OnMinigameFail += HidingMinigame_OnMinigameSuccessOrFail;

        Hide();
    }

    private void Update()
    {
        if (!isWaitingToShow) return;

        currentWaitingTime -= Time.deltaTime;

        if (currentWaitingTime <= 0f)
        {
            Debug.Log("B. Cooldown done, resuming + showing");
            isWaitingToShow = false;
            HidingMinigame.instance.ResumeMinigame();
            Show();
        }
    }

    private void HidingMinigame_OnMinigameSuccessOrFail(object sender, System.EventArgs e)
    {
        Debug.Log("A. Minigame result received, hiding UI");
        Hide();
        currentWaitingTime = waitingTimeMax;
        isWaitingToShow = true;
    }

    private void Player_OnPlayerInHidingSpot(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Player_OnPlayerExitHidingSpot(object sender, System.EventArgs e)
    {
        isWaitingToShow = false;
        Hide();
    }

    private void Show()
    {
        hidingMinigameUI.SetActive(true);
    }

    private void Hide()
    {
        hidingMinigameUI.SetActive(false);
    }
}
