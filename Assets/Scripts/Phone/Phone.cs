using UnityEngine;

public class Phone : MonoBehaviour
{
    public void Interact()
    {
        if (Player.instance)
        {
            Player.instance.SetPlayerHasPhone(true);
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
