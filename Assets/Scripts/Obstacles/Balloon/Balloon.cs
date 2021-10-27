using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Balloon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _score;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;

    public event UnityAction<int> BalloonShot;

    public int Damage => _damage;

    public void TakeBulletHit()
    {
        BalloonShot?.Invoke(_score);
        Burst();
    }

    public void TakeOffGun()
    {
        _animator.SetTrigger(BalloonAnimator.TakeOffGun);
    }

    private void Burst()
    {
        _animator.SetTrigger(BalloonAnimator.Burst);
        _particleSystem.Play();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
