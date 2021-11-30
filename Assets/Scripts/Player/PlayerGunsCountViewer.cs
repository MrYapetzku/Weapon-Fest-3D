using TMPro;
using UnityEngine;

public class PlayerGunsCountViewer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _player.GunsChanged += OnPlayerGunsCountChanged;
    }

    private void OnDisable()
    {
        _player.GunsChanged -= OnPlayerGunsCountChanged;
    }

    private void OnPlayerGunsCountChanged(int value)
    {
        if (value == 0)
        {
            _text.text = "";
            return;
        }
        _text.text = value.ToString();
    }
}
