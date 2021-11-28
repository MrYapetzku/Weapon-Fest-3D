using System;
using UnityEngine;

public class FinishingState : IGameState
{
    private ResultHandler _resultHandler;
    private Animator _cameraAnimator;
    private Animator _playerAnimator;

    public FinishingState(ResultHandler resultHandler, PlayerTracker mainCameraContainer, Player player)
    {
        _resultHandler = resultHandler;

        _cameraAnimator = mainCameraContainer.GetComponent<Animator>();
        if (_cameraAnimator == null)
            throw new Exception($"Main camera container doesn't contain component {typeof(Animator)}");

        _playerAnimator = player.GetComponent<Animator>();
        if (_playerAnimator == null)
            throw new Exception($"Player doesn't contain component {typeof(Animator)}");
    }

    public void Enter()
    {
        _cameraAnimator.SetTrigger(MainCameraAnimator.Finishing);
        _playerAnimator.SetTrigger(PlayerAnimator.Finishing);
    }

    public void Exit()
    {
        _cameraAnimator.ResetTrigger(MainCameraAnimator.Finishing);
        _playerAnimator.ResetTrigger(PlayerAnimator.Finishing);
        _resultHandler.enabled = false;
    }
}
