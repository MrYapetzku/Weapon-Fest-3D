using System;
using UnityEngine;

public class PlayState : IGameState
{
    private ResultHandler _resultHandler;
    private Animator _cameraAnimator;
    private PlayerMover _playerMover;
    private Shooting _shooting;
    private GameMenu _gameMenu;

    public PlayState(ResultHandler resultHandler, PlayerTracker mainCameraContainer, Player player, UI uI)
    {
        _resultHandler = resultHandler;
        _cameraAnimator = mainCameraContainer.GetComponent<Animator>();
        if (_cameraAnimator == null)
            throw new Exception($"Main camera container doesn't contain component {typeof(Animator)}");

        _playerMover = player.GetComponent<PlayerMover>();
        if (_playerMover == null)
            throw new Exception($"Player doesn't contain component {typeof(PlayerMover)}");

        _shooting = player.GetComponent<Shooting>();
        if (_shooting == null)
            throw new Exception($"Player doesn't contain component {typeof(Shooting)}");

        _gameMenu = uI.GameMenu;
    }

    public void Enter()
    {
        _resultHandler.enabled = true;
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
