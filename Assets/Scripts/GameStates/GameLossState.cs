public class GameLossState : IGameState
{
    private GameLossMenu _gameLossMenu;

    public GameLossState(GameLossMenu gameLossMenu)
    {
        _gameLossMenu = gameLossMenu;
    }

    public void Enter()
    {
        _gameLossMenu.gameObject.SetActive(true);
    }

    public void Exit()
    {
        _gameLossMenu.gameObject.SetActive(false);
    }
}
