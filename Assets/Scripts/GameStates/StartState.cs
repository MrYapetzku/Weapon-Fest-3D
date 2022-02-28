using UnityEngine;

public class StartState : IGameState
{
    private LevelLoader _levelLoader;
    private ResultHandler _resultHandler;
    private MainCameraContainer _mainCameraContainer;
    private Player _player;
    private Animator _playerAnimator;
    private Animator _cameraAnimator;
    private MainMenu _mainMenu;
    private PlayerInput _playerInput;

    public StartState(LevelLoader levelLoader, ResultHandler resultHandler, MainCameraContainer mainCameraContainer, Player player, UI uI, PlayerInput playerInput)
    {
        _levelLoader = levelLoader;
        _resultHandler = resultHandler;
        _mainCameraContainer = mainCameraContainer;
        _player = player;
        _mainMenu = uI.MainMenu;
        _playerInput = playerInput;
        _playerAnimator = player.Animator;
        _cameraAnimator = mainCameraContainer.Animator;
    }

    public void Enter()
    {
        _resultHandler.ResultsLoaded += OnResultsLoaded;
        _levelLoader.LevelLoaded += OnLevelLoaded;
        _resultHandler.Load();
        _mainCameraContainer.transform.position = Vector3.zero;
        _cameraAnimator.SetTrigger(MainCameraAnimator.Reset);
        _player.transform.position = Vector3.zero;
        _player.ResetGuns();
        _playerAnimator.SetTrigger(PlayerAnimator.Idle);
        _mainMenu.gameObject.SetActive(true);
        _mainCameraContainer.Wind_FX.gameObject.SetActive(true);
        _mainCameraContainer.PlayerTracker.enabled = true;
        _mainCameraContainer.FinalShotAnimationMove.enabled = false;
    }

    public void Exit()
    {
        _resultHandler.ResultsLoaded -= OnResultsLoaded;
        _levelLoader.LevelLoaded -= OnLevelLoaded;
        _cameraAnimator.ResetTrigger(MainCameraAnimator.Reset);
        _playerAnimator.ResetTrigger(PlayerAnimator.Idle);
        _mainMenu.gameObject.SetActive(false);
    }

    private void OnLevelLoaded()
    {
        //Fader.Instance.FadeOut(() => _playerInput.UI.Enable());
        _playerInput.UI.Enable();
    }

    private void OnResultsLoaded()
    {
        _levelLoader.Load(_resultHandler.CurrentLevel);
    }
}
