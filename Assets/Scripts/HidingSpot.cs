using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [SerializeField]
    private Transform door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Player Entered!");
            player.SetPlayerIsHiding(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Player Exited!");
            player.SetPlayerIsHiding(false);
        }
    }
}
