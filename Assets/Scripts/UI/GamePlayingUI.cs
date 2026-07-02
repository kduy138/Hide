using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI timeTxt;
    [SerializeField]
    private Image crosshair;

    private void Update()
    {
        //timeTxt.text = GameManager.instance.GetFormattedTime();

        if (GameManager.instance.GetCurrentState() != GameManager.State.GamePlaying)
        {
            crosshair.enabled = false;
        }
        else
        {
            crosshair.enabled = true;
        }
    }
}
