using UnityEngine;

public class BulletBlocker : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponentInParent<Bullet>();
        if (bullet)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}
