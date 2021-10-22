using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedState : IGameState
{
    private PlayerTracker _playerTracker;
    private EndLevelMenu _endLevelMenu;

    public FinishedState(PlayerTracker mainCameraContainer, EndLevelMenu endLevelMenu)
    {
        _playerTracker = mainCameraContainer;
        _endLevelMenu = endLevelMenu;
    }


    public void Enter()
    {
        _endLevelMenu.gameObject.SetActive(true);
    }

    public void Exit()
    {
        _playerTracker.enabled = true;
        _endLevelMenu.gameObject.SetActive(false);
    }
}
