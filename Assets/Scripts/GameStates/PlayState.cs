using System;
using UnityEngine;

public class PlayState : IGameState
{
    private ScoreCollector _scoreCollector;
    private Animator _cameraAnimator;
    private PlayerMover _playerMover;
    private Shooting _shooting;
    private GameMenu _gameMenu;

    public PlayState(ScoreCollector scoreCollector, PlayerTracker mainCameraContainer, Player player, GameMenu gameMenu)
    {
        _scoreCollector = scoreCollector;
        _cameraAnimator = mainCameraContainer.GetComponent<Animator>();
        if (_cameraAnimator == null)
            throw new Exception($"Main camera container doesn't contain component {typeof(Animator)}");

        _playerMover = player.GetComponent<PlayerMover>();
        if (_playerMover == null)
            throw new Exception($"Player doesn't contain component {typeof(PlayerMover)}");

        _shooting = player.GetComponent<Shooting>();
        if (_shooting == null)
            throw new Exception($"Player doesn't contain component {typeof(Shooting)}");

        _gameMenu = gameMenu;
    }

    public void Enter()
    {
        _scoreCollector.enabled = true;
        _cameraAnimator.SetTrigger(MainCameraAnimator.Start);
        _playerMover.enabled = true;
        _shooting.enabled = true;
        _gameMenu.gameObject.SetActive(true);
    }

    public void Exit()
    {
        _cameraAnimator.ResetTrigger(MainCameraAnimator.Start);
        _playerMover.enabled = false;
        _shooting.enabled = false;
        _gameMenu.gameObject.SetActive(false);
    }
}
