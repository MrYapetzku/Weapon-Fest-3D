using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter(Collider other)
    {
        var balloon = other.GetComponentInParent<Balloon>();
        if (balloon)
        {
            balloon.Deactivate();
            _player.DecreaseGunsCountBy(balloon.Damage);
        }
    }
}
