using UnityEngine;

public class MonopolyCam : MonoBehaviour
{
    [SerializeField]
    private float cameraSensX;
    [SerializeField]
    private float cameraSensY;

    [SerializeField]
    private Transform orientation;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (GameManager.instance.GetCurrentState() != GameManager.State.GamePlaying) return;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraSensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraSensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
