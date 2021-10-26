using TMPro;
using UnityEngine;

public class ScoreCollector : MonoBehaviour
{
    [SerializeField] private GameObjectsContainer _gameObjectsContainer;
    [SerializeField] private TMP_Text _currentGameScoresText;
    //Глобальные очки, чтобы не забыть потом доработать. Или удалить.
    //[SerializeField] private TMP_Text _globalScoresText;

    private Balloon[] _balloons;
    private int _currentGameCollectedScores;
    private int _scores;

    private GameData _gameData;
    private Storage _storage;

    private void Awake()
    {
        _storage = new Storage();
        _gameData = new GameData();
    }


    private void OnEnable()
    {
        _balloons = _gameObjectsContainer.GetComponentsInChildren<Balloon>();
        foreach (var baloon in _balloons)
        {
            baloon.BalloonShot += OnBalloonShot;
        }

        Load();
    }

    private void OnDisable()
    {
        foreach (var baloon in _balloons)
        {
            baloon.BalloonShot -= OnBalloonShot;
        }
    }

    private void OnBalloonShot(int balloonScores)
    {
        _currentGameCollectedScores += balloonScores;
        _currentGameScoresText.text = _currentGameCollectedScores.ToString();
    }

    private void Save()
    {
        _gameData.Scores = _scores;
        _storage.Save(_gameData);
    }

    private void Load()
    {
        _gameData = (GameData)_storage.Load(new GameData());
        _scores = _gameData.Scores;
    }
}
