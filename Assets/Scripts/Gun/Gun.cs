using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField] private ShootPoint _shootPoint;

    private Shooting _shooting;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _shooting = GetComponentInParent<Shooting>();
        if (_shooting == null)
            throw new System.Exception($"Parant object doesn't contain component {typeof(Shooting)}");
    }

    private void OnEnable()
    {
        _shooting.GunsShooting += OnGunsShooting;
    }

    private void OnDisable()
    {
        _shooting.GunsShooting -= OnGunsShooting;
    }

    private void OnGunsShooting()
    {
        Bullet bullet = _shooting.BulletPool.GetFreeElement();
        bullet.transform.position = _shootPoint.transform.position;
        _animator.SetTrigger(GunAnimator.Shoot);
    }
}
