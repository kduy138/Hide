using UnityEngine;

[RequireComponent(typeof(Animator))]

public class HeadLook : MonoBehaviour
{
    [Header("Look Settings")]
    [SerializeField] private float lookDistance = 5f;
    [SerializeField] private float viewAngle = 120f;
    [SerializeField] private float smoothSpeed = 5f;

    private Animator animator;

    private float currentWeight;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Camera.main == null)
        {
            currentWeight = Mathf.MoveTowards(
                currentWeight,
                0,
                smoothSpeed * Time.deltaTime);

            return;
        }

        Vector3 dir = Camera.main.transform.position - transform.position;

        float distance = dir.magnitude;

        float angle = Vector3.Angle(transform.forward, dir);

        bool canLook =
            distance <= lookDistance &&
            angle <= viewAngle * 0.5f;

        float targetWeight = canLook ? 1f : 0f;

        currentWeight = Mathf.MoveTowards(
            currentWeight,
            targetWeight,
            smoothSpeed * Time.deltaTime);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtWeight(
            currentWeight,
            0.2f,
            0.8f,
            1f,
            0.5f);

        if (Camera.main != null)
            animator.SetLookAtPosition(Camera.main.transform.position);
    }
}
