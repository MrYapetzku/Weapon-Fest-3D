using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FinalShotBulletChecker))]
public class Shooting : MonoBehaviour
{
    [SerializeField] private BulletContainer _bulletContainer;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] [Min(1)] private int _poolCount;
    [SerializeField] [Min(0)] private float _shootingTimeDelta;

    public PoolMono<Bullet> BulletPool;

    private float _currentTime;
    private bool _isFinalFire;
    private FinalShotBulletChecker _finalShotBulletChecker;

    public event UnityAction<bool> Fire;

    private void Awake()
    {
        _finalShotBulletChecker = GetComponent<FinalShotBulletChecker>();
        BulletPool = new PoolMono<Bullet>(_bulletTemplate, _bulletContainer.transform, _poolCount);
        _currentTime = _shootingTimeDelta;
    }

    private void OnEnable()
    {
        _isFinalFire = false;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            Fire?.Invoke(_isFinalFire);
            _currentTime = _shootingTimeDelta;
        }
    }

    public void DoFinalFire()
    {
        _isFinalFire = true;
        Fire?.Invoke(_isFinalFire);
        _finalShotBulletChecker.enabled = true;
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }
}
