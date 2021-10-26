using UnityEngine;
using UnityEngine.Events;

public class Balloon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _score;

    public event UnityAction<int> BalloonShot;

    public int Damage => _damage;


    public void TakeBulletHit()
    {
        BalloonShot?.Invoke(_score);
        Burst();
    }

    public void TakeOffGun()
    {
        gameObject.SetActive(false);
    }

    private void Burst()
    {
        gameObject.SetActive(false);
    }
}
