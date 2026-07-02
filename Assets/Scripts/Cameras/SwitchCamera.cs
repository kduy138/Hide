using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour
{
    public static SwitchCamera instance { get; private set; }

    [Header("References")]
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private GameObject monopolyCamera;

    [Header("Settings")]
    private int cameraIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Switch()
    {
        Debug.LogError("Switching Camera!");
        if (cameraIndex == 0)
        {
            PlayerCamera();
            cameraIndex = 1;
        }
        else
        {
            MonopolyCamera();
            cameraIndex = 0;
        }
    }

    private void MonopolyCamera()
    {
        monopolyCamera.SetActive(true);
        playerCamera.SetActive(false);
    }

    private void PlayerCamera()
    {
        monopolyCamera.SetActive(false);
        playerCamera.SetActive(true);
    }
}
