using UnityEngine;
using UnityEngine.Events;

public class Balloon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _score;

    public event UnityAction<int> BalloonShot;

    public int Damage => _damage;


    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.GetComponentInParent<Bullet>();
        if (bullet)
        {
            BalloonShot?.Invoke(_score);
            bullet.gameObject.SetActive(false);
            Deactivate();
        }
    }
}
