using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelSettings", menuName = "Level Settings")]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private Sprite _background;
    [SerializeField] private LevelEnvironment _levelEnvironmentTemplate;
    [SerializeField] private LevelGameObjects _levelGameObjectsTemplate;

    public Sprite Background => _background;
    public LevelEnvironment LevelEnvironment => _levelEnvironmentTemplate;
    public LevelGameObjects LevelGameObjects => _levelGameObjectsTemplate;
}
