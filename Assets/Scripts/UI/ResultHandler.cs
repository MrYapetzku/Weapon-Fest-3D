using UnityEngine;

public class ResultHandler : MonoBehaviour
{
    [SerializeField] private GameObjectsContainer _gameObjectsContainer;

    private Balloon[] _balloons;
    private int _currentGameScores;
    private int _allCollectedScores;
    private int _currentLevel;

    private GameData _gameData;
    private Storage _storage;

    public int CurrentGameScores => _currentGameScores;
    public int AllCollectedScores => _allCollectedScores;
    public int CurrentLevel => _currentLevel;

    private void Awake()
    {
        _balloons = new Balloon[0];
        _storage = new Storage();
        _gameData = new GameData();
    }

    private void OnEnable()
    {
        _balloons = _gameObjectsContainer.GetComponentsInChildren<Balloon>();
        _currentGameScores = 0;
        foreach (var baloon in _balloons)
        {
            baloon.BalloonShot += OnBalloonShot;
        }
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
        _currentGameScores += balloonScores;
    }

    public void Save()
    {
        _allCollectedScores += _currentGameScores;
        _gameData.Scores = _allCollectedScores;
        _gameData.CurrentLevel = _currentLevel;
        _storage.Save(_gameData);
    }

    public void Load()
    {
        _gameData = (GameData)_storage.Load(new GameData());
        _allCollectedScores = _gameData.Scores;
        _currentLevel = _gameData.CurrentLevel;
    }
}
