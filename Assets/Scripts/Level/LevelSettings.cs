using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelSettings", menuName = "Level Settings")]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private Texture _background;
    [SerializeField] private LevelEnvironment _levelEnvironmentTemplate;
    [SerializeField] private LevelGameObjects _levelGameObjectsTemplate;

    public Texture Background => _background;
    public LevelEnvironment LevelEnvironment => _levelEnvironmentTemplate;
    public LevelGameObjects LevelGameObjects => _levelGameObjectsTemplate;
}
