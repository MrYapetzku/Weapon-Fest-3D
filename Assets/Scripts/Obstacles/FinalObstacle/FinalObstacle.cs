using UnityEngine;
using UnityEngine.Events;

public class FinalObstacle : MonoBehaviour
{
    [SerializeField] [Min(0)] private int _durability;
    [SerializeField] [Min(0)] private float _scoreMultiplier;
    [SerializeField] private Material _brokenMaterial;

    private MeshRenderer _meshRenderer;
    private Collider _collider;
    private Balloon[] _balloons;
    private float _discreteDurablity;
    private int _discreteDurablityStep;
    private int _burstBalloonIndex;

    public event UnityAction<float> Broken;

    public int Durability => _durability;

    private void Start()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _collider = GetComponentInChildren<BoxCollider>();
        _balloons = GetComponentsInChildren<Balloon>();
        _discreteDurablityStep = _durability / _balloons.Length;
        _discreteDurablity = _durability - _discreteDurablityStep;
        _burstBalloonIndex = 0;
    }

    public int TakeBulletHitAndReturnExcess(int bulletDuplicatesCount)
    {
        _meshRenderer.material = _brokenMaterial;

        if (bulletDuplicatesCount < _durability)
        {
            _durability -= bulletDuplicatesCount;
            BurstBalloons();
            return 0;
        }
        else
        {
            bulletDuplicatesCount -= _durability;
            _durability = 0;
            BurstBalloons();
            SetBrokenState();
            return bulletDuplicatesCount;
        }
    }

    private void BurstBalloons()
    {
        while (_durability < _discreteDurablity && _burstBalloonIndex >= 0)
        {
            _balloons[_burstBalloonIndex].Burst();
            _burstBalloonIndex++;
            _discreteDurablity -= _discreteDurablityStep;
        }
    }

    private void SetBrokenState()
    {
        Broken?.Invoke(_scoreMultiplier);
        _collider.enabled = false;
    }
}
