using UnityEngine;
using UnityEngine.Events;

public class BossBalloon : MonoBehaviour
{
    public event UnityAction BossHitted;

    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.GetComponentInParent<Bullet>();

        if (bullet)
            BossHitted?.Invoke();
    }
}
