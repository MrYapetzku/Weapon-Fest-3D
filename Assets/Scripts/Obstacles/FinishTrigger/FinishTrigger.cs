using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private BulletBlocker _bulletBlocker;

    private Shooting _shooting;

    private void OnTriggerEnter(Collider other)
    {
        _shooting = other.GetComponentInParent<Shooting>();
        if (_shooting)
        {
            _shooting.Fire += OnFire;
        }
    }

    private void OnFire(bool isFinalFire)
    {
        if (isFinalFire)
        {
            _shooting.Fire -= OnFire;
            _bulletBlocker.gameObject.SetActive(false);
        }
    }
}
