using System;
using UnityEngine;

public class StartState : IGameState
{
    private LevelLoader _levelLoader;
    private ScoreCollector _scoreCollector;
    private PlayerTracker _mainCameraContainer;
    private Player _player;
    private Animator _playerAnimator;
    private Animator _cameraAnimator;
    private MainMenu _mainMenu;

    public StartState(LevelLoader levelLoader, ScoreCollector scoreCollector, PlayerTracker mainCameraContainer, Player player, MainMenu mainMenu)
    {
        _levelLoader = levelLoader;
        _scoreCollector = scoreCollector;
        _mainCameraContainer = mainCameraContainer;
        _player = player;
        _mainMenu = mainMenu;

        _playerAnimator = player.GetComponent<Animator>();
        if (_playerAnimator == null)
            throw new Exception($"Player doesn't contain component {typeof(Animator)}");

        _cameraAnimator = mainCameraContainer.GetComponent<Animator>();
        if (_cameraAnimator == null)
            throw new Exception($"Main camera container doesn't contain component {typeof(Animator)}");
    }

    public void Enter()
    {
        _levelLoader.Load(0);
        _scoreCollector.enabled = false;
        _mainCameraContainer.transform.position = Vector3.zero;
        _cameraAnimator.SetTrigger(MainCameraAnimator.MainMenu);
        _player.transform.position = Vector3.zero;
        _player.ResetPlayerGunsCount();
        _playerAnimator.SetTrigger(PlayerAnimator.Idle);
        _mainMenu.gameObject.SetActive(true);

        _mainCameraContainer.GetComponent<PlayerTracker>().enabled = true;
        _mainCameraContainer.GetComponent<FinalShotAnimationMove>().enabled = false;
    }

    public void Exit()
    {
        _cameraAnimator.ResetTrigger(MainCameraAnimator.MainMenu);
        _playerAnimator.ResetTrigger(PlayerAnimator.Idle);
        _mainMenu.gameObject.SetActive(false);
    }
}
