using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Start Stats")]
    [SerializeField] [Min(1)] private int _startGuns;
    [Space]
    [Header("Player Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private GunPointsContainer _gunPointsContainer;
    [SerializeField] private PlayerViewModel _playerViewModel;
    [SerializeField] private PlayerColliderScaler _playerCollider;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Shooting _shooting;

    private int _guns;

    public event UnityAction<int> GunsChanged;
    public event UnityAction GameLoss;
    public event UnityAction LevelFinishing;
    public event UnityAction LevelFinished;

    public Animator Animator => _animator;
    public GunPointsContainer GunPointsContainer => _gunPointsContainer;
    public PlayerMover PlayerMover => _playerMover;
    public Shooting Shooting => _shooting;

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

    public void ChangeGunsTo(int value)
    {
        if (value > 0)
        {
            Guns = value;
        }
        else
        {
            Guns = 0;
            GameLoss?.Invoke();
        }
    }

    public void ResetGuns()
    {
        Guns = _startGuns;
    }
}
