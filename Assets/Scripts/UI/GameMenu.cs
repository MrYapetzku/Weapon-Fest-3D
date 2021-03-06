using TMPro;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private ResultHandler _resultHandler;
    [SerializeField] private TMP_Text _currentGameScoresViewer;
    [SerializeField] [Min(0)] private int _maxScoreClamp;

    private void Update()
    {
        int result = Mathf.Clamp(_resultHandler.CurrentGameScores, 0, _maxScoreClamp);

        _currentGameScoresViewer.text = result.ToString();
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
