using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Start Stats")]
    [SerializeField] private int _startGuns;
    [Space]
    [Header("Player Components")]
    [SerializeField] GunPointsContainer _gunPointsContainer;
    [SerializeField] PlayerViewModel _playerViewModel;
    [SerializeField] PlayerColliderScaler _playerCollider;

    private int _guns;

    public event UnityAction<int> GunsChanged;
    public event UnityAction GameLoss;
    public event UnityAction LevelFinishing;
    public event UnityAction LevelFinished;

    public GunPointsContainer GunPointsContainer => _gunPointsContainer;

    public int Guns
    {
        get => _guns;
        private set
        {
            _guns = value;
            GunsChanged?.Invoke(value);
        }
    }

    private void Awake()
    {
        _gunPointsContainer.gameObject.SetActive(true);
        _playerViewModel.gameObject.SetActive(true);
        _playerCollider.gameObject.SetActive(true);
    }

    private void Start()
    {
        ResetGuns();
    }

    public void OnLevelFinishing()
    {
        LevelFinishing?.Invoke();
    }

    public void OnLevelFinished()
    {
        LevelFinished?.Invoke();
    }

    public void ChangeGunsBy(int value)
    {
        if (value >= 0)
        {
            Guns += value;
        }
        else
        {
            if (_guns > -value)
            {
                Guns += value;
            }
            else
            {
                Guns = 0;
                GameLoss?.Invoke();
            }
        }
    }

    public void ResetGuns()
    {
        Guns = _startGuns;
    }
}
