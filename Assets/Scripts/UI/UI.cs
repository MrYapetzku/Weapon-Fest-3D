using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private EndLevelMenu _endLevelMenu;
    [SerializeField] private GameLossMenu _gameLossMenu;

    public MainMenu MainMenu => _mainMenu;
    public GameMenu GameMenu => _gameMenu;
    public EndLevelMenu EndLevelMenu => _endLevelMenu;
    public GameLossMenu GameLossMenu => _gameLossMenu;
}
