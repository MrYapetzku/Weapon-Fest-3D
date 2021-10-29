using UnityEngine;
using UnityEngine.Events;

public class ResultHandler : MonoBehaviour
{
    [SerializeField] private GameObjectsContainer _gameObjectsContainer;

    private Balloon[] _balloons;
    private FinalObstacle[] _finalObstacles;
    private int _currentGameScores;
    private float _currentScoreMultiplier;
    private int _allCollectedScores;
    private int _currentLevel;

    private GameData _gameData;
    private Storage _storage;

    public event UnityAction ResultsLoaded;

    public int CurrentGameScores => _currentGameScores;
    public int AllCollectedScores => _allCollectedScores;
    public int CurrentLevel => _currentLevel;

    private void Awake()
    {
        _balloons = new Balloon[0];
    }

    private void OnEnable()
    {
        _currentGameScores = 0;
        _currentScoreMultiplier = 1;

        _balloons = _gameObjectsContainer.GetComponentsInChildren<Balloon>();
        foreach (var baloon in _balloons)
            baloon.BalloonShot += OnBalloonShot;

        _finalObstacles = _gameObjectsContainer.GetComponentsInChildren<FinalObstacle>();
        foreach (var obstacle in _finalObstacles)
            obstacle.Broken += OnBroken;
    }

    private void OnDisable()
    {
        _currentGameScores = (int)(_currentGameScores * _currentScoreMultiplier);
        foreach (var baloon in _balloons)
            baloon.BalloonShot -= OnBalloonShot;

        foreach (var obstacle in _finalObstacles)
            obstacle.Broken -= OnBroken;
    }

    private void OnBroken(float multiplier)
    {
        _currentScoreMultiplier += multiplier;
    }

    private void OnBalloonShot(int balloonScores)
    {
        _currentGameScores += balloonScores;
    }

    public void Save()
    {
        _allCollectedScores += _currentGameScores;
        _gameData.Scores = _allCollectedScores;
        _gameData.CurrentLevel = ++_currentLevel;
        _storage.Save(_gameData);
    }

    public void Load()
    {
        if (_storage == null)
            _storage = new Storage();

        _gameData = (GameData)_storage.Load(new GameData());
        _allCollectedScores = _gameData.Scores;
        _currentLevel = _gameData.CurrentLevel;
        ResultsLoaded?.Invoke();
    }
}
