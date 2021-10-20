using UnityEngine;

public class GameOverTest : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.GameOver += OnGameOver;
        _player.LevelFinished += OnLevelFinished;
    }

    private void OnDisable()
    {
        _player.GameOver -= OnGameOver;
        _player.LevelFinished -= OnLevelFinished;
    }

    private void OnGameOver()
    {
        _player.gameObject.SetActive(false);
        Debug.Log("Game Over");
    }

    private void OnLevelFinished()
    {
        _player.gameObject.SetActive(false);
        Debug.Log("Level Finished");
    }
}
