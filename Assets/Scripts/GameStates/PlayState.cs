using UnityEngine;

public class PlayState : IGameState
{
    private ResultHandler _resultHandler;
    private Animator _cameraAnimator;
    private PlayerMover _playerMover;
    private Shooting _shooting;
    private GameMenu _gameMenu;

    public PlayState(ResultHandler resultHandler, MainCameraContainer mainCameraContainer, Player player, UI uI)
    {
        _resultHandler = resultHandler;
        _cameraAnimator = mainCameraContainer.Animator;
        _playerMover = player.PlayerMover;
        _shooting = player.Shooting;
        _gameMenu = uI.GameMenu;
    }

    public void Enter()
    {
        _resultHandler.Enable();
        _cameraAnimator.SetTrigger(MainCameraAnimator.Start);
        _playerMover.Enable();
        _shooting.Enable();
        _gameMenu.SetActive(true);
    }

    public void Exit()
    {
        _cameraAnimator.ResetTrigger(MainCameraAnimator.Start);
        _playerMover.Disable();
        _shooting.Disable();
        _gameMenu.SetActive(false);
    }
}
