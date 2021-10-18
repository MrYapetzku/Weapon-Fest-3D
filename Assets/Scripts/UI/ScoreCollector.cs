using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCollector : MonoBehaviour
{
    [SerializeField] private BalloonContainer _balloonContainer;
    [SerializeField] private TMP_Text _collectedScoresText;

    private Balloon[] _balloons;
    private int _collectedScores;

    private void Awake()
    {
        _balloons = _balloonContainer.GetComponentsInChildren<Balloon>();
    }

    private void OnEnable()
    {
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
