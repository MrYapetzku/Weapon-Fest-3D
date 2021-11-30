using UnityEngine;

public class GameObjectsContainer : MonoBehaviour
{
    [SerializeField] private BulletContainer _bulletContainer;

    public BulletContainer BulletContainer => _bulletContainer;
}
