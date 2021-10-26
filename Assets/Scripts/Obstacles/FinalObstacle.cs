using UnityEngine;
using UnityEngine.Events;

// ���������� ������� ����������� � ����� � ����.
public class FinalObstacle : MonoBehaviour
{
    [SerializeField] private int _durability;
    [SerializeField] private float _scoreMultiplier;
    [SerializeField] private Material _brokenMaterial;

    private MeshRenderer _meshRenderer;
    private Collider _collider;

    public event UnityAction<float> Broken;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
    }

    public void TakeBulletHit()
    {
        _durability--;
        if (_durability < 1)
        {
            SetBrokenState();
        }
    }

    private void SetBrokenState()
    {
        _collider.enabled = false;
        _meshRenderer.material = _brokenMaterial;
        Broken?.Invoke(_scoreMultiplier);
    }
}
