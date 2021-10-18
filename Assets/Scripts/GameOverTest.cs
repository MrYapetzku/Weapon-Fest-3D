using UnityEngine;

public class GameOverTest : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _player.GameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        _player.gameObject.SetActive(false);
        Debug.Log("Game Over");
    }
}
