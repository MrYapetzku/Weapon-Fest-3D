using System;
using UnityEngine;

public class StartState : IGameState
{
    private PlayerTracker _mainCameraContainer;
    private Player _player;
    private MainMenu _mainMenu;
    private Animator _cameraAnimator;

    public StartState(PlayerTracker mainCameraContainer, Player player, MainMenu mainMenu)
    {
        _mainCameraContainer = mainCameraContainer;
        _player = player;
        _mainMenu = mainMenu;

        _cameraAnimator = _mainCameraContainer.GetComponent<Animator>();
        if (_cameraAnimator == null)
            throw new Exception($"Main camera container doesn't contain component {typeof(Animator)}");
    }

    public void Enter()
    {
        _mainCameraContainer.transform.position = Vector3.zero;
        _cameraAnimator.SetTrigger(MainCameraAnimator.MainMenu);
        _player.transform.position = Vector3.zero;
        _player.ResetPlayerGunsCount();
        _mainMenu.gameObject.SetActive(true);
    }

    public void Exit()
    {
        _cameraAnimator.ResetTrigger(MainCameraAnimator.MainMenu);
        _mainMenu.gameObject.SetActive(false);
    }
}
