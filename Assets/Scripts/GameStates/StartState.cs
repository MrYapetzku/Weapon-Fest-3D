using System;
using UnityEngine;

public class StartState : IGameState
{
    private LevelLoader _levelLoader;
    private ResultHandler _resultHandler;
    private PlayerTracker _mainCameraContainer;
    private Player _player;
    private Animator _playerAnimator;
    private Animator _cameraAnimator;
    private MainMenu _mainMenu;
    private PlayerInput _playerInput;

    public StartState(LevelLoader levelLoader, ResultHandler resultHandler, PlayerTracker mainCameraContainer, Player player, MainMenu mainMenu, PlayerInput playerInput)
    {
        _levelLoader = levelLoader;
        _resultHandler = resultHandler;
        _mainCameraContainer = mainCameraContainer;
        _player = player;
        _mainMenu = mainMenu;
        _playerInput = playerInput;

        _playerAnimator = player.GetComponent<Animator>();
        if (_playerAnimator == null)
            throw new Exception($"Player doesn't contain component {typeof(Animator)}");

        _cameraAnimator = mainCameraContainer.GetComponent<Animator>();
        if (_cameraAnimator == null)
            throw new Exception($"Main camera container doesn't contain component {typeof(Animator)}");
    }

    public void Enter()
    {
        _resultHandler.ResultsLoaded += OnResultsLoaded;
        _levelLoader.LevelGameObjectsLoaded += OnLevelGameObjectsLoaded;
        _resultHandler.Load();
        _mainCameraContainer.transform.position = Vector3.zero;
        _cameraAnimator.SetTrigger(MainCameraAnimator.Reset);
        _player.transform.position = Vector3.zero;
        _player.ResetPlayerGunsCount();
        _playerAnimator.SetTrigger(PlayerAnimator.Idle);
        _mainMenu.gameObject.SetActive(true);
        _mainCameraContainer.GetComponent<PlayerTracker>().enabled = true;
        _mainCameraContainer.GetComponent<FinalShotAnimationMove>().enabled = false;
    }

    public void Exit()
    {
        _resultHandler.ResultsLoaded -= OnResultsLoaded;
        _levelLoader.LevelGameObjectsLoaded -= OnLevelGameObjectsLoaded;
        _cameraAnimator.ResetTrigger(MainCameraAnimator.Reset);
        _playerAnimator.ResetTrigger(PlayerAnimator.Idle);
        _mainMenu.gameObject.SetActive(false);
    }

    private void OnLevelGameObjectsLoaded()
    {
        _playerInput.UI.Enable();
    }

    private void OnResultsLoaded()
    {
        _levelLoader.Load(_resultHandler.CurrentLevel);
    }
}
