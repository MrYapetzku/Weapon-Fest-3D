using UnityEngine;

public class FinishingState : IGameState
{
    private ResultHandler _resultHandler;
    private Animator _cameraAnimator;
    private Animator _playerAnimator;

    public FinishingState(ResultHandler resultHandler, MainCameraContainer mainCameraContainer, Player player)
    {
        _resultHandler = resultHandler;
        _cameraAnimator = mainCameraContainer.Animator;
        _playerAnimator = player.Animator;
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
        _resultHandler.Disable();
    }
}
