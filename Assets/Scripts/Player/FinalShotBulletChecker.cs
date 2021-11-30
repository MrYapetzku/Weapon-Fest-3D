using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinalShotBulletChecker : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private GameObjectsContainer _gameObjectsContainer;

    private List<Bullet> _bullets;
    private BossBalloon _bossBalloon;
    private Animator _cameraAnimator;

    private void Awake()
    {
        _bullets = new List<Bullet>();
        _cameraAnimator = Camera.main.GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        _bullets.AddRange(_gameObjectsContainer.BulletContainer.GetComponentsInChildren<Bullet>());
        _bossBalloon = _gameObjectsContainer.GetComponentInChildren<BossBalloon>();
        if (_bossBalloon)
            _bossBalloon.BossHitted += OnBossHitted;
    }

    private void Update()
    {
        CheckBulletsCount();
    }

    private void OnBossHitted()
    {
        _bossBalloon.BossHitted -= OnBossHitted;
        _cameraAnimator.SetTrigger(MainCameraAnimator.LookAtBoss);
        enabled = false;
    }

    private void CheckBulletsCount()
    {
        if (GetActiveBulletsCount() < 1)
        {
            _player.OnLevelFinished();
            enabled = false;
        }
    }

    private int GetActiveBulletsCount()
    {
        return _bullets.Where(b => b.isActiveAndEnabled).ToList().Count;
    }
}
