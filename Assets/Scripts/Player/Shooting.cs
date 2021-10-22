using UnityEngine;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{
    [SerializeField] private BulletContainer _bulletContainer;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private int _poolCount;
    [SerializeField] private float _shootingTimeDelta;

    public PoolMono<Bullet> BulletPool;

    private float _currentTime;

    public event UnityAction Fire;

    private void Awake()
    {
        BulletPool = new PoolMono<Bullet>(_bulletTemplate, _bulletContainer.transform, _poolCount);
        _currentTime = _shootingTimeDelta;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            Fire?.Invoke();
            _currentTime = _shootingTimeDelta;
        }
    }
}
