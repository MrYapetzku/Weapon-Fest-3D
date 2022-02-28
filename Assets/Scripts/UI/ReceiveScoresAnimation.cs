using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ReceiveScoresAnimation : MonoBehaviour
{
    [SerializeField] private Image _emissionCoinImage;
    [SerializeField] private Image _receivingCoinImage;
    [SerializeField] private int _emissionCoinsCount;
    [SerializeField] private float _emissionRadius;
    [SerializeField] [Range(0, 1)] float _translationPositionFactor;
    [SerializeField] [Range(0, 1)] float _emitionPositionFactor;

    private Animator _animator;
    private List<Image> _emittedCoins;
    private List<Vector3> _emitTargetPositions;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _emittedCoins = new List<Image>();
        _emitTargetPositions = new List<Vector3>();

        for (int i = 0; i < _emissionCoinsCount; i++)
        {
            Image coin = Instantiate(_emissionCoinImage, _emissionCoinImage.transform.position, Quaternion.identity, transform);
            _emittedCoins.Add(coin);
        }
    }

    private void Update()
    {
        SetTranslationPositions();
        AddEnitionOffset();
    }

    private void AddEnitionOffset()
    {
        for (int i = 0; i < _emittedCoins.Count; i++)
        {
            _emittedCoins[i].transform.position += Vector3.Lerp(Vector3.zero, _emitTargetPositions[i], _emitionPositionFactor);
        }
    }

    private void SetTranslationPositions()
    {
        foreach (var coin in _emittedCoins)
            coin.transform.position = Vector3.Lerp(_emissionCoinImage.transform.position, _receivingCoinImage.transform.position, _translationPositionFactor);
    }

    private void OnEnable()
    {
        GenerateEmitTargetPositions();
    }

    private void GenerateEmitTargetPositions()
    {
        _emitTargetPositions.Clear();
        for (int i = 0; i < _emissionCoinsCount; i++)
            _emitTargetPositions.Add(Random.insideUnitCircle * _emissionRadius);
    }
}
