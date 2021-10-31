using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerColliderScaler : MonoBehaviour
{
    [SerializeField] private float _minPlayerColliderRadius;

    private Player _player;
    private SphereCollider _playerCollider;
    private GunsPointGiver _gunsPointGiver;
    private List<int> _pointCountsInLayers;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _playerCollider = GetComponent<SphereCollider>();
        _gunsPointGiver = GetComponentInParent<GunsPointGiver>();
    }

    private void Start()
    {
        _pointCountsInLayers = _gunsPointGiver.GetPointCountsInLayers();
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

        for (int i = 0; i < _pointCountsInLayers.Count - 1; i++)
        {
            if (playerGunsCount > _pointCountsInLayers[i])
            {
                _playerCollider.radius += _gunsPointGiver.DistanceBetweenGuns;
            }
        }

        if (_playerCollider.radius < _minPlayerColliderRadius)
            _playerCollider.radius = _minPlayerColliderRadius;
    }
}
