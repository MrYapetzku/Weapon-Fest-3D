using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "NewLevelSettings", menuName = "Level Settings")]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private Material _skyBoxMaterial;
    [SerializeField] private LevelEnvironment _levelEnvironmentTemplate;
    [SerializeField] private AssetReference _levelGameObjectsTemplate;
    [SerializeField] private Color _fogColor;

    public LevelEnvironment LevelEnvironment => _levelEnvironmentTemplate;
    public AssetReference LevelGameObjects => _levelGameObjectsTemplate;
    public Material SkyBoxMaterial => _skyBoxMaterial;
    public Color FogColor => _fogColor;
}
