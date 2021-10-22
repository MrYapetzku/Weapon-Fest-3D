using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover), typeof(Shooting))]
public class Player : MonoBehaviour
{
    [SerializeField] private GunsContainer _gunsContainer;
    [SerializeField] private Gun _gunTemplate;
    [SerializeField] private int _poolCount;

    private List<Gun> _playerGuns;
    private PoolMono<Gun> _gunsPool;

    public event UnityAction<int> PlayerGunsCountChanged;
    public event UnityAction GameLoss;
    public event UnityAction LevelFinishing;
    public event UnityAction LevelFinished;

    public int GunsCount => _playerGuns.Count;

    private void Start()
    {
        _gunsPool = new PoolMono<Gun>(_gunTemplate, _gunsContainer.transform, _poolCount);
        _playerGuns = new List<Gun>();
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
            _playerGuns.Add(_gunsPool.GetFreeElement());
        PlayerGunsCountChanged?.Invoke(_playerGuns.Count);
    }

    public void DecreaseGunsCountBy(int value)
    {
        if (_playerGuns.Count > value)
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
            ResetPlayerGunsCount();
            GameLoss?.Invoke();
        }
    }

    public void ResetPlayerGunsCount()
    {
        if (_playerGuns == null)
            return;

        foreach (var gun in _playerGuns)
            gun.gameObject.SetActive(false);
        _playerGuns.Clear();

        _playerGuns.Add(_gunsPool.GetFreeElement());
        PlayerGunsCountChanged?.Invoke(_playerGuns.Count);
    }
}
