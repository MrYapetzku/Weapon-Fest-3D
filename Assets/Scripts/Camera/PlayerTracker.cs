using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _distanceToPlayerZ;

    private void Start()
    {
        _distanceToPlayerZ = _player.transform.position.z - transform.position.z;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z - _distanceToPlayerZ);
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }

    private void OnLevelFinished()
    {
        _player.OnLevelFinished();
    }
}
