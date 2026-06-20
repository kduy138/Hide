using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Image timeBar;
    [SerializeField]
    private Button tryAgainBtn;
    [SerializeField]
    private Button mainMenuBtn;

    private void Start()
    {
        Player.instance.OnPlayerExitHidingSpot += Player_OnPlayerExitHidingSpot;
        HidingMinigame.instance.OnMinigameSuccess += HidingMinigame_OnMinigameSuccessOrFail;
        HidingMinigame.instance.OnMinigameFail += HidingMinigame_OnMinigameSuccessOrFail;
        GameManager.instance.OnHidingMinigameStarted += GameManager_OnMinigameStarted;
        HidingMinigame.instance.OnMinigameLose += HidingMinigame_OnMinigameLose;

        tryAgainBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        mainMenuBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MenuScene);
        });

        HideMinigameUI();
        HideMinigameFailedUI();
    }

    private void Update()
    {
        UpdateTimerBar();

        if (!isWaitingToShow) return;

        currentWaitingToShowTime -= Time.deltaTime;

        if (currentWaitingToShowTime <= 0f)
        {
            isWaitingToShow = false;
            HidingMinigame.instance.ResumeMinigame();
            ShowMinigameUI();
        }
    }

    private void UpdateTimerBar()
    {
        timeBar.fillAmount = HidingMinigame.instance.TimeBarFillAmount();

        if (HidingMinigame.instance.GetCurrentMinigameTime() <= 0)
        {
            timeBar.fillAmount = 0f;
        }
    }

    private void HidingMinigame_OnMinigameLose(object sender, System.EventArgs e)
    {
        ShowMinigameFailedUI();
        HideMinigameUI();
    }

    private void GameManager_OnMinigameStarted(object sender, System.EventArgs e)
    {
        ShowMinigameUI();
        HidingMinigame.instance.StartTimer();
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
