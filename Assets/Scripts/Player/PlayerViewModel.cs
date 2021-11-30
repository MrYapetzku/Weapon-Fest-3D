using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewModel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Gun _gunTemplate;
    [SerializeField] [Min(0)] private int _poolCount;
    [SerializeField] [Min(0)] private float _appearGunDuration;
    [SerializeField] [Min(0)] private float _appearSphereRadius;
    [SerializeField] private float _appearSpherePositionZ;
    [SerializeField] [Min(0)] private int _duplicateAnimationGunsCount;

    private PoolMono<Gun> _pool;
    private List<Gun> _displayedGuns;
    private int _currentGunsCount;
    private int _gunToDuplicateIndex;


    private void Awake()
    {
        _displayedGuns = new List<Gun>();
        _pool = new PoolMono<Gun>(_gunTemplate, transform, _poolCount);
        InitPool();
        _currentGunsCount = 0;
        _gunToDuplicateIndex = 0;
    }

    private void OnEnable()
    {
        _player.GunsChanged += OnGunsChanged;
    }

    private void OnDisable()
    {
        _player.GunsChanged -= OnGunsChanged;
    }

    private void InitPool()
    {
        foreach (var gun in _pool.Pool)
        {
            gun.Initialization(_appearGunDuration, _appearSphereRadius, _appearSpherePositionZ);
        }
    }

    private void OnGunsChanged(int count)
    {
        if (_currentGunsCount < count)
            IncreaseGunsTo(count);
        else if (_currentGunsCount > count)
            DecreaseGunsTo(count);
    }

    private void IncreaseGunsTo(int count)
    {
        for (int i = _currentGunsCount; i < count; i++)
            AddGun();
        if (_currentGunsCount > _player.GunPointsContainer.GunPointsCount)
            StartCoroutine(PlayDuplicateGunAnimation());
    }

    private void DecreaseGunsTo(int count)
    {
        for (int i = _currentGunsCount; i > count; i--)
            RemoveGun();
    }

    private void AddGun()
    {
        if (_displayedGuns.Count < _player.GunPointsContainer.GunPointsCount)
        {
            Gun gun = _pool.GetFreeElement();
            _displayedGuns.Add(gun);
            _currentGunsCount++;
        }
        else
        {
            _displayedGuns[_gunToDuplicateIndex].IncreaseDuplicateByOne();
            _currentGunsCount++;
            _gunToDuplicateIndex++;
            if (_gunToDuplicateIndex == _displayedGuns.Count)
                _gunToDuplicateIndex -= _displayedGuns.Count;
        }
    }

    private void RemoveGun()
    {
        if (_currentGunsCount <= _displayedGuns.Count)
        {
            Gun gun = _displayedGuns[_displayedGuns.Count - 1];
            gun.gameObject.SetActive(false);
            _displayedGuns.Remove(gun);
            _currentGunsCount--;
        }
        else
        {
            if (_gunToDuplicateIndex == 0)
                _gunToDuplicateIndex += _displayedGuns.Count;
            _gunToDuplicateIndex--;
            _displayedGuns[_gunToDuplicateIndex].DecreaseDuplicateByOne();
            _currentGunsCount--;
        }
    }

    private IEnumerator PlayDuplicateGunAnimation()
    {
        List<Gun> guns = new List<Gun>();

        for (int i = 0; i < _duplicateAnimationGunsCount; i++)
        {
            guns.Add(_pool.GetFreeElement());
        }
        yield return new WaitForSeconds(_appearGunDuration);

        foreach (var gun in guns)
            gun.gameObject.SetActive(false);
        guns.Clear();
    }
}
