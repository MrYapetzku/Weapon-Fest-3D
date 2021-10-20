using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Shooting))]
public class Player : MonoBehaviour
{
    [SerializeField] private GunsContainer _gunsContainer;
    [SerializeField] private Gun _gunTemplate;
    [SerializeField] private int _poolCount;

    private List<Gun> _playerGuns;
    private PoolMono<Gun> _gunsPool;

    public event UnityAction<int> PlayerGunsCountChanged;
    public event UnityAction GameOver;
    public event UnityAction LevelFinished;

    public int GunsCount => _playerGuns.Count;

    private void Start()
    {
        _gunsPool = new PoolMono<Gun>(_gunTemplate, _gunsContainer.transform, _poolCount);
        _playerGuns = new List<Gun>();
        _playerGuns.Add(_gunsPool.GetFreeElement());
        PlayerGunsCountChanged?.Invoke(_playerGuns.Count);
    }

    private void Update()
    {        
    }

    public void Finish()
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
            foreach (var gun in _playerGuns)
                gun.gameObject.SetActive(false);
            _playerGuns.Clear();
            PlayerGunsCountChanged?.Invoke(_playerGuns.Count);
            GameOver?.Invoke();
        }
    }
}
