using UnityEngine;

public class HeadLook : MonoBehaviour
{
    [SerializeField]
    private Transform headObj;
    [SerializeField]
    private Transform targetObj;

    private Vector3 offsetEuler = new Vector3(0, -90, 0);

    private void LateUpdate()
    {
        Vector3 dir = targetObj.position - headObj.position;

        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.up);
        Quaternion offset = Quaternion.Euler(offsetEuler);

        headObj.rotation = lookRotation * Quaternion.Inverse(offset);
    }
}
