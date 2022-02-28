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
