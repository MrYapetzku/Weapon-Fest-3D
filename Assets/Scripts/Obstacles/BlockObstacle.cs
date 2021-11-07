using UnityEngine;

public class BlockObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponentInParent<Bullet>();
        if (bullet)
            bullet.gameObject.SetActive(false);
    }
}
