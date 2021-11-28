using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelSettings", menuName = "Level Settings")]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private Material _skyBoxMaterial;
    [SerializeField] private LevelEnvironment _levelEnvironmentTemplate;
    [SerializeField] private LevelGameObjects _levelGameObjectsTemplate;
    [SerializeField] private Color _fogColor;

    public LevelEnvironment LevelEnvironment => _levelEnvironmentTemplate;
    public LevelGameObjects LevelGameObjects => _levelGameObjectsTemplate;
    public Material SkyBoxMaterial => _skyBoxMaterial;
    public Color FogColor => _fogColor;
}
