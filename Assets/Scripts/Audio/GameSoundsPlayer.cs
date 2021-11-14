using UnityEngine;

public class GameSoundsPlayer : MonoBehaviour
{
    [SerializeField] private ClipPlayer _balloonSoundPlayer;
    [SerializeField] private GameObjectsContainer _gameObjectsContainer;
    [SerializeField] private ClipPlayer _shootSoundPlayer;
    [SerializeField] private Shooting _playerShooting;

    private Balloon[] _balloons;

    private void OnDisable()
    {
        Release();
    }

    public void Init()
    {
        _balloons = _gameObjectsContainer.GetComponentsInChildren<Balloon>();
        foreach (var baloon in _balloons)
            baloon.BalloonBursting += OnBalloonBursting;
        _playerShooting.Fire += OnFire;
    }

    public void Release()
    {
        foreach (var baloon in _balloons)
            baloon.BalloonBursting -= OnBalloonBursting;
        _playerShooting.Fire -= OnFire;
    }

    private void OnBalloonBursting()
    {
        _balloonSoundPlayer.Play();
    }

    private void OnFire(bool isFinalFire)
    {
        _shootSoundPlayer.Play();
    }
}
