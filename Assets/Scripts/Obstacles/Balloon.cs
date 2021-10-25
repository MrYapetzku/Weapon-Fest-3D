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

    public void Hit()
    {
        BalloonShot?.Invoke(_score);
        gameObject.SetActive(false);
    }
}
