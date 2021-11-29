using UnityEngine;
using UnityEngine.Events;

public class Balloon : MonoBehaviour
{
    [SerializeField][Min(0)] private int _damage;
    [SerializeField][Min(0)] private int _score;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Collider _collider;

    public event UnityAction<int> BalloonShot;
    public event UnityAction BalloonBursting;

    public int Damage => _damage;

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
        BalloonBursting?.Invoke();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
