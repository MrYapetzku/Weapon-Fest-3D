using UnityEngine;
using UnityEngine.Events;

// Доработать наличие компонентов и связь с ними.
public class FinalObstacle : MonoBehaviour
{
    [SerializeField] private int _durability;
    [SerializeField] private float _scoreMultiplier;
    [SerializeField] private Material _brokenMaterial;

    private MeshRenderer _meshRenderer;
    private Collider _collider;

    public event UnityAction<float> Broken;

    public int Durability => _durability;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
    }

    public void TakeBulletHit()
    {
        _durability--;
        if (_durability < 1)
            SetBrokenState();
    }

    private void SetBrokenState()
    {
        _collider.enabled = false;
        _meshRenderer.material = _brokenMaterial;
        Broken?.Invoke(_scoreMultiplier);
    }
}
