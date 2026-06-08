using TMPro;
using UnityEngine;

public class GamePlayingUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI timeTxt;

    private void Update()
    {
        timeTxt.text = GameManager.instance.GetFormattedTime();
    }
}
