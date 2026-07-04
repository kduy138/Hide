using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour, IEventObj
{
    public bool start = false;
    [SerializeField]
    private float shakeDuration;
    [SerializeField]
    private AnimationCurve curve;

    private bool isTriggered = false;

    public void TriggerEvent()
    {
        if (!isTriggered)
        {
            StartCoroutine(Shaking());
            isTriggered = true;
        } 
    }

    private IEnumerator Shaking()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < shakeDuration) {
            elapsedTime += Time.deltaTime;
            float strenth = curve.Evaluate(elapsedTime / shakeDuration);
            transform.position = startPos + Random.insideUnitSphere * strenth;
            yield return null;
        }

        transform.position = startPos;
    }
}
