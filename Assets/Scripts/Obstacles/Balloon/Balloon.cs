using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(SphereCollider))]
public class Balloon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _score;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;

    private Collider _collider;

    public event UnityAction<int> BalloonShot;

    public int Damage => _damage;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public void TakeBulletHit()
    {
        _collider.enabled = false;
        BalloonShot?.Invoke(_score);
        Burst();
    }

    public void TakeOffGun()
    {
        _animator.SetTrigger(BalloonAnimator.TakeOffGun);
    }

    public void Burst()
    {
        _animator.SetTrigger(BalloonAnimator.Burst);
        _particleSystem.Play();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
