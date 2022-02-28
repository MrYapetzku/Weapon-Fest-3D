using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Colorant : MonoBehaviour
{
    [SerializeField] private Material _firstPlatformMaterial;
    [SerializeField] private Gradient _gradient;

    private List<MeshRenderer> _meshRenderers = new List<MeshRenderer>();
    private float _gradientStep;
    private int _oldCount;

    private void Start()
    {
        if (Application.IsPlaying(this))
        {
            CorrectColor();
            Destroy(this);
        }
        _oldCount = transform.childCount;
    }

    private void Update()
    {
        if (_oldCount != transform.childCount)
        {
            _oldCount = transform.childCount;
            CorrectColor();
        }
    }

    private void OnValidate()
    {
        CorrectColor();
    }

    private void CorrectColor()
    {
        _meshRenderers.Clear();

        FinalObstaclePlatform[] finalObstaclePlatforms = GetComponentsInChildren<FinalObstaclePlatform>();

        for (int i = 0; i < finalObstaclePlatforms.Length; i++)
        {
            _meshRenderers.Add(finalObstaclePlatforms[i].GetComponentInChildren<MeshRenderer>());
        }

        _meshRenderers[0].material = _firstPlatformMaterial;

        if (_meshRenderers.Count > 1)
        {
            _gradientStep = 1.0f / (_meshRenderers.Count - 1);
            for (int i = 1; i < _meshRenderers.Count; i++)
            {
                Material material = new Material(_firstPlatformMaterial);

                material.color = _gradient.Evaluate(_gradientStep * i);

                _meshRenderers[i].material = material;
            }
        }
    }
}
