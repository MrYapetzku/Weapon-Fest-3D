using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Shooting))]
public class Player : MonoBehaviour
{
    [SerializeField] private Gun _gunTemplate;
    [SerializeField] private int _poolCount;
    [SerializeField] private float _maxGunsCircleRadius;
    [SerializeField] private float _gunsCircleExpansionRatio;

    private List<Gun> _playerGuns;
    private PoolMono<Gun> _gunsPool;

    public event UnityAction<int> PlayerGunsCountChanged;
    public event UnityAction GameOver;

    public int GunsCount => _playerGuns.Count;

    private void Awake()
    {
        _gunsPool = new PoolMono<Gun>(_gunTemplate, transform, _poolCount);
        _playerGuns = new List<Gun>();
        SetGun(Vector3.zero);
        PlayerGunsCountChanged?.Invoke(_playerGuns.Count);
    }

    public void IncreaseGunsCountBy(int value)
    {
        for (int i = 0; i < value; i++)
            SetGun(GenerateGunPlacePoint());
        PlayerGunsCountChanged?.Invoke(_playerGuns.Count);
    }

    public void DecreaseGunsCountBy(int value)
    {
        if (_playerGuns.Count - value > 0)
        {
            for (int i = 0; i < value; i++)
            {
                Gun gun = _playerGuns[_playerGuns.Count - 1];
                gun.gameObject.SetActive(false);
                _playerGuns.Remove(gun);
            }
            PlayerGunsCountChanged?.Invoke(_playerGuns.Count);
        }
        else
        {
            foreach (var gun in _playerGuns)
                gun.gameObject.SetActive(false);
            _playerGuns.Clear();
            PlayerGunsCountChanged?.Invoke(_playerGuns.Count);
            GameOver?.Invoke();
        }
    }

    private Vector3 GenerateGunPlacePoint()
    {
        Vector3 point = Random.insideUnitCircle * GetGunsCircleRadius();
        return transform.position + point;
    }

    private float GetGunsCircleRadius()
    {
        return Mathf.Clamp(_playerGuns.Count * _gunsCircleExpansionRatio, 0, _maxGunsCircleRadius);
    }

    private void SetGun(Vector3 point)
    {
        Gun getedGun = _gunsPool.GetFreeElement();
        _playerGuns.Add(getedGun);
        getedGun.transform.position = point;
    }
}
