using UnityEngine;

public class FinalShotAnimationMove : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    private void Update()
    {
        transform.Translate(Vector3.forward * _bulletPrefab.FinalShootSpeed * Time.deltaTime);
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }
}
