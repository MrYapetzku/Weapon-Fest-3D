using UnityEngine;

public class FadeInHandler : MonoBehaviour
{
    [SerializeField] private GameStatesSwitcher _gameStatesSwitcher;

    public void HandleFadeIn()
    {
        Fader.Instance.FadeIn(() => _gameStatesSwitcher.SetStartState());
    }
}
