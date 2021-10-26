using System.Collections;
using TMPro;
using UnityEngine;

public class EndLevelMenu : MonoBehaviour
{
    [SerializeField] private ResultHandler _resultHandler;
    [SerializeField] private TMP_Text _currentGameScoreViewer;
    [SerializeField] private TMP_Text _allGameScoresViewer;
    [SerializeField] private float _addResultAnimationDuration;
    [SerializeField] private float _addResultAnimationDelay;

    private int _currentGameScores;
    private int _allScores;

    private void OnEnable()
    {
        _currentGameScores = _resultHandler.CurrentGameScores;
        _allScores = _resultHandler.AllCollectedScores;

        ShowResult();
        StartCoroutine(ShowAddResultAnimation());
    }

    private void OnDisable()
    {
        StopCoroutine(ShowAddResultAnimation());
    }

    private IEnumerator ShowAddResultAnimation()
    {
        float timer = _addResultAnimationDuration;
        int startAnimationCurrentGameScores = _currentGameScores;
        int startAnimationAllScores = _allScores;

        yield return new WaitForSeconds(_addResultAnimationDelay);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                timer = 0;
            _currentGameScores = (int)Mathf.Lerp(0, startAnimationCurrentGameScores, timer / _addResultAnimationDuration);
            _allScores = startAnimationAllScores + _currentGameScores;

            ShowResult();

            yield return new WaitForEndOfFrame();
        }
    }

    private void ShowResult()
    {
        _currentGameScoreViewer.text = _currentGameScores.ToString();
        _allGameScoresViewer.text = _allScores.ToString();
    }
}
