using UnityEngine;

public class FinalObstacle : MonoBehaviour
{
    [SerializeField] private int _durability;
    [SerializeField] private Material _brokenMaterial;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void TakeBulletHit()
    {
        _durability--;
        if (_durability < 1)
        {
            this.enabled = false;
            SetBrokenMaterial();
        }
    }

    private void SetBrokenMaterial()
    {
        _meshRenderer.material = _brokenMaterial;
    }
}
