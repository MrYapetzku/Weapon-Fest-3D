public class GameLossState : IGameState
{
    private Player _player;
    private GameLossMenu _gameLossMenu;

    public GameLossState(Player player, GameLossMenu gameLossMenu)
    {
        _player = player;
        _gameLossMenu = gameLossMenu;
    }

    public void Enter()
    {
        _player.gameObject.SetActive(false);
        _gameLossMenu.gameObject.SetActive(true);
    }

    public void Exit()
    {
        _player.gameObject.SetActive(true);
        _gameLossMenu.gameObject.SetActive(false);
    }
}
