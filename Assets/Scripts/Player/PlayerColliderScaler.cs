using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerColliderScaler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SphereCollider _playerCollider;
    [SerializeField] private float _minPlayerColliderRadius;

    private void OnEnable()
    {
        _player.GunsChanged += OnPlayerGunsCountChanged;
    }

    private void OnDisable()
    {
        _player.GunsChanged -= OnPlayerGunsCountChanged;
    }

    private void OnPlayerGunsCountChanged(int playerGunsCount)
    {
        _playerCollider.radius = _player.GunPointsContainer.DistanceBetweenGuns;

        for (int i = 0; i < _player.GunPointsContainer.PointCountsInLayers.Count - 1; i++)
        {
            if (playerGunsCount > _player.GunPointsContainer.PointCountsInLayers[i])
            {
                _playerCollider.radius += _player.GunPointsContainer.DistanceBetweenGuns;
            }
        }

        if (_playerCollider.radius < _minPlayerColliderRadius)
            _playerCollider.radius = _minPlayerColliderRadius;
    }
}
