public class GameLossState : IGameState
{
    private ResultHandler _resultHandler;
    private GameLossMenu _gameLossMenu;
    private Wind_FX _wind_FX;

    public GameLossState(ResultHandler resultHandler, MainCameraContainer mainCameraContainer, UI uI)
    {
        _resultHandler = resultHandler;
        _gameLossMenu = uI.GameLossMenu;
        _wind_FX = mainCameraContainer.Wind_FX;
    }

    public void Enter()
    {
        _resultHandler.Disable();
        _gameLossMenu.SetActive(true);
    }

    public void Exit()
    {
        _gameLossMenu.SetActive(false);
        _wind_FX.SetActive(false);
    }
}
