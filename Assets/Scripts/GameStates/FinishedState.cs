using System;
using UnityEngine;

public class FinishedState : IGameState
{
    private ResultHandler _resultHandler;
    private PlayerTracker _playerTracker;
    private EndLevelMenu _endLevelMenu;
    private Animator _cameraAnimator;
    private Animator _playerAnimator;
    private Wind_FX _wind_FX;

    public FinishedState(ResultHandler resultHandler, PlayerTracker mainCameraContainer, Player player, EndLevelMenu endLevelMenu)
    {
        _resultHandler = resultHandler;
        _playerTracker = mainCameraContainer;
        _endLevelMenu = endLevelMenu;

        _cameraAnimator = mainCameraContainer.GetComponent<Animator>();
        if (_cameraAnimator == null)
            throw new Exception($"Main camera container doesn't contain component {typeof(Animator)}");

        _playerAnimator = player.GetComponent<Animator>();
        if (_playerAnimator == null)
            throw new Exception($"Player doesn't contain component {typeof(Animator)}");

        _wind_FX = mainCameraContainer.GetComponentInChildren<Wind_FX>();
        if (_wind_FX == null)
            throw new Exception($"Main cameras children doesn't contain component {typeof(Wind_FX)}");
    }

    public void Enter()
    {
        _endLevelMenu.gameObject.SetActive(true);
        _resultHandler.Save();
        _cameraAnimator.SetTrigger(MainCameraAnimator.Finished);
        _playerAnimator.SetTrigger(PlayerAnimator.Idle);
    }

    public void Exit()
    {
        _playerTracker.enabled = true;
        _endLevelMenu.gameObject.SetActive(false);
        _cameraAnimator.ResetTrigger(MainCameraAnimator.Finished);
        _playerAnimator.ResetTrigger(PlayerAnimator.Idle);
        _wind_FX.gameObject.SetActive(false);
    }
}
