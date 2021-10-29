using System;
using UnityEngine;

public class FinishedState : IGameState
{
    private ResultHandler _resultHandler;
    private PlayerTracker _playerTracker;
    private EndLevelMenu _endLevelMenu;
    private Animator _cameraAnimator;
    private Animator _playerAnimator;

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
    }

    public void Enter()
    {
        _endLevelMenu.gameObject.SetActive(true);
        _resultHandler.Save();
        _playerTracker.GetComponent<FinalShotAnimationMove>().enabled = false;
        _cameraAnimator.SetTrigger(MainCameraAnimator.Finished);
        _playerAnimator.SetTrigger(PlayerAnimator.Idle);
    }

    public void Exit()
    {
        _playerTracker.enabled = true;
        _endLevelMenu.gameObject.SetActive(false);
        _cameraAnimator.ResetTrigger(MainCameraAnimator.Finished);
        _playerAnimator.ResetTrigger(PlayerAnimator.Idle);
    }
}
