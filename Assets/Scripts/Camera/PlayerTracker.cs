using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private bool _enableXTracking;

    private float _distanceToPlayerZ;

    private void Start()
    {
        _distanceToPlayerZ = _player.transform.position.z - transform.position.z;
    }

    private void Update()
    {
        if (_enableXTracking)
            transform.position = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z - _distanceToPlayerZ);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z - _distanceToPlayerZ);
    }
}
