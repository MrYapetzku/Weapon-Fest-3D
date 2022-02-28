using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ShootPoint _shootPoint;
    [SerializeField] private ParticleSystem _particleSystem;

    private Shooting _shooting;
    private int _duplicates;

    public float AppearDuration { get; private set; }
    public float AppearSphereRadius { get; private set; }
    public float AppearSpherePositionZ { get; private set; }

    private void Awake()
    {
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

    public void Initialization(float time, float radius, float zFactor)
    {
        if (time > 0)
            AppearDuration = time;
        if (radius >= 0)
            AppearSphereRadius = radius;
        AppearSpherePositionZ = zFactor;
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

    private void OnFire(bool isFinalFire)
    {
        Bullet bullet = _shooting.BulletPool.GetFreeElement();
        bullet.SetDuplicates(_duplicates);
        if (isFinalFire)
            bullet.SetFinalShotSettings();
        bullet.transform.position = _shootPoint.transform.position;
        _animator.SetTrigger(GunAnimator.Shoot);
        _particleSystem.Play();
    }
}
