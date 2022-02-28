using UnityEngine;

public class FinishedState : IGameState
{
    private ResultHandler _resultHandler;
    private PlayerTracker _playerTracker;
    private EndLevelMenu _endLevelMenu;
    private Animator _cameraAnimator;
    private Animator _playerAnimator;
    private Wind_FX _wind_FX;

    public FinishedState(ResultHandler resultHandler, MainCameraContainer mainCameraContainer, Player player, UI uI)
    {
        _resultHandler = resultHandler;
        _playerTracker = mainCameraContainer.PlayerTracker;
        _endLevelMenu = uI.EndLevelMenu;
        _cameraAnimator = mainCameraContainer.Animator;
        _playerAnimator = player.Animator;
        _wind_FX = mainCameraContainer.Wind_FX;
    }

    public void Enter()
    {
        _endLevelMenu.SetActive(true);
        _resultHandler.Save();
        _cameraAnimator.SetTrigger(MainCameraAnimator.Finished);
        _playerAnimator.SetTrigger(PlayerAnimator.Idle);
    }

    public void Exit()
    {
        _playerTracker.Enable();
        _endLevelMenu.SetActive(false);
        _cameraAnimator.ResetTrigger(MainCameraAnimator.Finished);
        _playerAnimator.ResetTrigger(PlayerAnimator.Idle);
        _wind_FX.SetActive(false);
    }
}
