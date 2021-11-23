using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerColliderScaler : MonoBehaviour
{
    [SerializeField] private float _minPlayerColliderRadius;

    private Player _player;
    private SphereCollider _playerCollider;
    private GunsPointGiver _gunsPointGiver;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _playerCollider = GetComponent<SphereCollider>();
        _gunsPointGiver = GetComponentInParent<GunsPointGiver>();
    }

    private void OnEnable()
    {
        _player.PlayerGunsCountChanged += OnPlayerGunsCountChanged;
    }

    private void OnDisable()
    {
        _player.PlayerGunsCountChanged -= OnPlayerGunsCountChanged;
    }

    private void OnPlayerGunsCountChanged(int playerGunsCount)
    {
        _playerCollider.radius = _gunsPointGiver.DistanceBetweenGuns;


        for (int i = 0; i < _gunsPointGiver.PointCountsInLayers.Count - 1; i++)
        {
            if (playerGunsCount > _gunsPointGiver.PointCountsInLayers[i])
            {
                _playerCollider.radius += _gunsPointGiver.DistanceBetweenGuns;
            }
        }

        if (_playerCollider.radius < _minPlayerColliderRadius)
            _playerCollider.radius = _minPlayerColliderRadius;
    }
}
