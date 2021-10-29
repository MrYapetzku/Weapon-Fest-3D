public class GameLossState : IGameState
{
    private ResultHandler _resultHandler;
    private Player _player;
    private GameLossMenu _gameLossMenu;

    public GameLossState(ResultHandler resultHandler, Player player, GameLossMenu gameLossMenu)
    {
        _resultHandler = resultHandler;
        _player = player;
        _gameLossMenu = gameLossMenu;
    }

    public void Enter()
    {
        _resultHandler.enabled = false;
        _player.gameObject.SetActive(false);
        _gameLossMenu.gameObject.SetActive(true);
    }

    public void Exit()
    {
        _player.gameObject.SetActive(true);
        _gameLossMenu.gameObject.SetActive(false);
    }
}
