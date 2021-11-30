using UnityEngine;

public class MainCameraContainer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerTracker _playerTracker;
    [SerializeField] private FinalShotAnimationMove _finalShotAnimationMove;
    [SerializeField] private Wind_FX _wind_FX;

    public Animator Animator => _animator;
    public PlayerTracker PlayerTracker => _playerTracker;
    public FinalShotAnimationMove FinalShotAnimationMove => _finalShotAnimationMove;
    public Wind_FX Wind_FX => _wind_FX;
}
