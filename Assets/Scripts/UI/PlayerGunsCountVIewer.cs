using TMPro;
using UnityEngine;

public class PlayerGunsCountVIewer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _player.PlayerGunsCountChanged += OnPlayerGunsCountChanged;
    }

    private void OnDisable()
    {
        _player.PlayerGunsCountChanged -= OnPlayerGunsCountChanged;
    }

    private void OnPlayerGunsCountChanged(int value)
    {
        _text.text = value.ToString();
    }
}
