using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour
{
    public static SwitchCamera instance { get; private set; }

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

    public void SwitchTo(GameObject targetCam, GameObject currentCam)
    {
        targetCam.SetActive(true);
        currentCam.SetActive(false);
    }
}
