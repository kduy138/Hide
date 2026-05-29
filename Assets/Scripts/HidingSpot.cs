using SojaExiles;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private MonoBehaviour doorBehaviour;
    private IDoor door;
    private Player playerInside = null;

    private void Awake()
    {
        door = doorBehaviour as IDoor;
    }

    private void Update()
    {
        if (playerInside == null) return;

        playerInside.SetPlayerIsHiding(!door.IsOpen());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (player != null)
            {
                playerInside = player;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (player != null)
            {
                playerInside.SetPlayerIsHiding(false);
                playerInside = null;
            }
        }
    }
}
