using UnityEngine;
using UnityEngine.Events;

public class FinalObstacle : MonoBehaviour
{
    [SerializeField] [Min(0)] private int _durability;
    [SerializeField] [Min(0)] private float _scoreMultiplier;
    [SerializeField] private Material _brokenMaterial;

    private MeshRenderer _meshRenderer;
    private Collider _collider;
    private Animator[] _balloonAnimators;
    private float _discreteDurablity;
    private int _discreteDurablityStep;
    private int _burstBalloonIndex;

    public event UnityAction<float> Broken;

    public int Durability => _durability;

    private void Start()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _collider = GetComponentInChildren<BoxCollider>();
        _balloonAnimators = GetComponentsInChildren<Animator>();
        _discreteDurablityStep = _durability / _balloonAnimators.Length;
        _discreteDurablity = _durability - _discreteDurablityStep;
        _burstBalloonIndex = 0;
    }

    public int TakeBulletHitAndReturnExcess(int bulletDuplicatesCount)
    {
        if (bulletDuplicatesCount < _durability)
        {
            _durability -= bulletDuplicatesCount;
            TryBurstBalloons();
            return 0;
        }
        else
        {
            bulletDuplicatesCount -= _durability;
            _durability = 0;
            TryBurstBalloons();
            SetBrokenState();
            return bulletDuplicatesCount;
        }
    }

    private void TryBurstBalloons()
    {
        while (_durability < _discreteDurablity && _burstBalloonIndex >= 0)
        {
            _balloonAnimators[_burstBalloonIndex].SetTrigger(BalloonAnimator.Burst);
            _burstBalloonIndex++;
            _discreteDurablity -= _discreteDurablityStep;
        }
    }

    private void SetBrokenState()
    {
        _collider.enabled = false;
        _meshRenderer.material = _brokenMaterial;
        Broken?.Invoke(_scoreMultiplier);
    }
}
