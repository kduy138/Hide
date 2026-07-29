using UnityEngine;

public class HeadLook : MonoBehaviour
{
    [SerializeField]
    private Transform headObj;
    [SerializeField]
    private Transform targetObj;
    [SerializeField]
    private Transform headForward;
    [SerializeField]
    private float minAngle;
    [SerializeField]
    private float maxAngle;

    private float lookSpeed = 7.0f;
    private Quaternion defaultRotation;

    private Vector3 offsetEuler = new Vector3(0, -90, 0);

    private void Start()
    {
        defaultRotation = headObj.rotation;
    }

    private void LateUpdate()
    {
        Vector3 dir = targetObj.position - headObj.position;
        if (dir.sqrMagnitude < 0.0001f) return;

        float angle = Vector3.SignedAngle(dir, headForward.forward, headForward.up);
        Debug.Log("Angle: " + angle);

        if (angle < maxAngle && angle > minAngle)
        {
            //Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
            Quaternion targetRotation = Quaternion.LookRotation(targetObj.position - headObj.position);
            Quaternion offset = Quaternion.Euler(offsetEuler);
            defaultRotation = Quaternion.Slerp(defaultRotation, targetRotation, lookSpeed * Time.deltaTime);
            headObj.rotation = defaultRotation * Quaternion.Inverse(offset);
        }
        else
        {
            defaultRotation = Quaternion.Slerp(defaultRotation, headForward.rotation, lookSpeed * Time.deltaTime);
            headObj.rotation = defaultRotation;
        }
    }
}
