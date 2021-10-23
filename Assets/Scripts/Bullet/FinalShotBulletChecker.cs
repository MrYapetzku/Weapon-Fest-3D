using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinalShotBulletChecker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private BulletContainer _bulletContainer;

    private List<Bullet> _bullets;

    private void Awake()
    {
        _bullets = new List<Bullet>();
    }

    private void OnEnable()
    {
        _bullets.AddRange(_bulletContainer.GetComponentsInChildren<Bullet>());
    }

    private void Update()
    {
        CheckBulletsCount();
    }

    private void CheckBulletsCount()
    {
        if (GetActiveBulletsCount() < 1)
        {
            _player.OnLevelFinished();
        }
    }

    private int GetActiveBulletsCount()
    {
        return _bullets.Where(b => b.isActiveAndEnabled).ToList().Count;
    }
}
