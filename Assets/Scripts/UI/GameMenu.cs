using TMPro;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private ResultHandler _resultHandler;
    [SerializeField] private TMP_Text _currentGameScoresViewer;

    private void Update()
    {
        _currentGameScoresViewer.text = _resultHandler.CurrentGameScores.ToString();
    }
}
