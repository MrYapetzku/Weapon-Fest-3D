using TMPro;
using UnityEngine;

[ExecuteAlways]
public class FinalObstacleTextSetter : MonoBehaviour
{
    [SerializeField] private float _scoreMultiplierValue;

    private TMP_Text[] _obstaclesTexts;
    private int _oldCount;

    private void Start()
    {
        if (Application.IsPlaying(this))
        {
            CorrectText();
            Destroy(this);
        }
        _oldCount = transform.childCount;
    }

    private void Update()
    {
        if (_oldCount != transform.childCount)
        {
            _oldCount = transform.childCount;
            CorrectText();
        }
    }

    private void OnValidate()
    {
        CorrectText();
    }

    private void CorrectText()
    {
        _obstaclesTexts = GetComponentsInChildren<TMP_Text>();

        _obstaclesTexts[0].text = "X1.0";
        _obstaclesTexts[1].text = "X1.0";

        for (int i = 2; i < _obstaclesTexts.Length; i += 2)
        {
            string obstacleText = $"X{(int)(1.0f + (i / 2) * _scoreMultiplierValue)}.{(int)(((1.0f + (i / 2) * _scoreMultiplierValue) % 1)*10)}";

            _obstaclesTexts[i].text = obstacleText;
            _obstaclesTexts[i + 1].text = obstacleText;
        }
    }
}
