using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;
    [SerializeField] private Player _player;
    [SerializeField] private GameObjectsContainer _gameObjectsContainer;
    
    private FinishTrigger _finish;

    private void OnEnable()
    {
        _finish = _gameObjectsContainer.GetComponentInChildren<FinishTrigger>();
        if (_finish == null)
            throw new System.Exception($"Level gameobjects prefab doesn't contain {typeof(FinishTrigger)}");
    }

    private void Update()
    {
        _progressBar.value = _player.transform.position.z / _finish.transform.position.z;
    }
}
