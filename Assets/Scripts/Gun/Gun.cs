using UnityEngine;

[RequireComponent(typeof(Animator), typeof(ShootPoint))]
public class Gun : MonoBehaviour
{
    private ShootPoint _shootPoint;
    private Animator _animator;
    private Shooting _shooting;
    private int _duplicates;

    private void Awake()
    {
        _shootPoint = GetComponent<ShootPoint>();
        _animator = GetComponent<Animator>();
        _shooting = GetComponentInParent<Shooting>();
        if (_shooting == null)
            throw new System.Exception($"Parant object doesn't contain component {typeof(Shooting)}");
    }

    private void OnEnable()
    {
        _duplicates = 1;
        _shooting.Fire += OnFire;
    }

    private void OnDisable()
    {
        _shooting.Fire -= OnFire;
    }

    public void IncreaseDuplicateByOne()
    {
        _duplicates++;
    }

    public void DecreaseDuplicateByOne()
    {
        _duplicates--;
        if (_duplicates < 1)
            gameObject.SetActive(false);
    }

    private void OnFire(float bulletLifeTime)
    {
        Bullet bullet = _shooting.BulletPool.GetFreeElement();
        bullet.SetDuplicates(_duplicates);
        bullet.SetLifetime(bulletLifeTime);
        bullet.transform.position = _shootPoint.transform.position;
        _animator.SetTrigger(GunAnimator.Shoot);
    }
}
