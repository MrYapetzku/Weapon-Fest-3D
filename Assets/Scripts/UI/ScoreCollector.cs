using TMPro;
using UnityEngine;

public class ScoreCollector : MonoBehaviour
{
    [SerializeField] private GameObjectsContainer _gameObjectsContainer;
    [SerializeField] private TMP_Text _collectedScoresText;

    private Balloon[] _balloons;
    private int _collectedScores;

    private void OnEnable()
    {
        _balloons = _gameObjectsContainer.GetComponentsInChildren<Balloon>();
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
        _collectedScores += balloonScores;
        _collectedScoresText.text = _collectedScores.ToString();
    }
}
