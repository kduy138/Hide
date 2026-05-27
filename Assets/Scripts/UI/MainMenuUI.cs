using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button startBtn;
    [SerializeField]
    private Button optionsBtn;
    [SerializeField]
    private Button quitBtn;

    private void Awake()
    {
        startBtn.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
