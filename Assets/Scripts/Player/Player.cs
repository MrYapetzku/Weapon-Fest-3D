using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover), typeof(Shooting), typeof(GunsPointGiver))]
public class Player : MonoBehaviour
{
    [SerializeField] private GunsContainer _gunsContainer;
    [SerializeField] private Gun _gunTemplate;
    [SerializeField] private int _poolCount;

    [SerializeField] private float _duplicateAnimationDuration;
    [SerializeField] private int _duplicateAnimationGunsCount;

    private PoolMono<Gun> _gunsPool;
    private List<Gun> _playerVisibleGuns;
    private int _playerGunsCount;
    private int _gunPointsCount;
    private int _gunToDuplicateIndex;

    public event UnityAction<int> PlayerGunsCountChanged;
    public event UnityAction GameLoss;
    public event UnityAction LevelFinishing;
    public event UnityAction LevelFinished;

    public int GunsCount => _playerGunsCount;

    private void Start()
    {
        _gunsPool = new PoolMono<Gun>(_gunTemplate, _gunsContainer.transform, _poolCount);
        _playerVisibleGuns = new List<Gun>();
        _gunPointsCount = GetComponent<GunsPointGiver>().GunPointsCount;
        ResetPlayerGunsCount();
    }

    private void Update()
    {
    }

    public void OnLevelFinishing()
    {
        LevelFinishing?.Invoke();
    }

    public void OnLevelFinished()
    {
        LevelFinished?.Invoke();
    }

    public void IncreaseGunsCountBy(int value)
    {
        for (int i = 0; i < value; i++)
            AddGun();
        PlayerGunsCountChanged?.Invoke(_playerGunsCount);
        if (_playerGunsCount > _gunPointsCount)
            StartCoroutine(PlayDuplicateGunAnimation());
    }

    public void DecreaseGunsCountBy(int value)
    {
        if (_playerGunsCount > value)
        {
            for (int i = 0; i < value; i++)
            {
                RemoveGun();
            }
            PlayerGunsCountChanged?.Invoke(_playerGunsCount);
        }
        else
        {
            ResetPlayerGunsCount();
            GameLoss?.Invoke();
        }
    }

    public void ResetPlayerGunsCount()
    {
        if (_playerVisibleGuns == null)
            return;

        foreach (var gun in _playerVisibleGuns)
            gun.gameObject.SetActive(false);
        _playerVisibleGuns.Clear();

        _gunToDuplicateIndex = 0;
        _playerGunsCount = 0;
        AddGun();
        PlayerGunsCountChanged?.Invoke(_playerGunsCount);
    }

    private void AddGun()
    {
        if (_playerVisibleGuns.Count < _gunPointsCount)
        {
            _playerVisibleGuns.Add(_gunsPool.GetFreeElement());
            _playerGunsCount++;
        }
        else
        {
            _playerVisibleGuns[_gunToDuplicateIndex].IncreaseDuplicateByOne();
            _playerGunsCount++;
            _gunToDuplicateIndex++;
            if (_gunToDuplicateIndex == _playerVisibleGuns.Count)
                _gunToDuplicateIndex -= _playerVisibleGuns.Count;
        }
    }

    private void RemoveGun()
    {
        if (_playerGunsCount <= _playerVisibleGuns.Count)
        {
            Gun gun = _playerVisibleGuns[_playerVisibleGuns.Count - 1];
            gun.gameObject.SetActive(false);
            _playerVisibleGuns.Remove(gun);
            _playerGunsCount--;
        }
        else
        {
            if (_gunToDuplicateIndex == 0)
                _gunToDuplicateIndex += _playerVisibleGuns.Count;
            _gunToDuplicateIndex--;
            _playerVisibleGuns[_gunToDuplicateIndex].DecreaseDuplicateByOne();
            _playerGunsCount--;
        }
    }

    private IEnumerator PlayDuplicateGunAnimation()
    {
        List<Gun> guns = new List<Gun>();

        for (int i = 0; i < _duplicateAnimationGunsCount; i++)
        {
            guns.Add(_gunsPool.GetFreeElement());
        }
        yield return new WaitForSeconds(_duplicateAnimationDuration);

        foreach (var gun in guns)
            gun.gameObject.SetActive(false);
        guns.Clear();
    }
}
