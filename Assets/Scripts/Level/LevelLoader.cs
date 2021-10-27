using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Background _background;
    [SerializeField] private EnvironmentContainer _environmentContainer;
    [SerializeField] private GameObjectsContainer _gameObjectsContainer;

    private LevelSettings[] _settings;
    private LevelEnvironment _loadedEnvironment;
    private LevelGameObjects _loadedLevelGameObjects;
    private Image _backgroundImage;

    private void Awake()
    {
        _settings = Resources.LoadAll<LevelSettings>("");
        if (_settings == null)
            throw new System.Exception("Level settings resources didn't load.");

        _backgroundImage = _background.GetComponent<Image>();
    }

    public void Load(int levelIndex)
    {
        if (levelIndex < 0 && levelIndex > _settings.Length)
            throw new System.Exception("Invalid level settings index.");

        if (_loadedEnvironment)
            Destroy(_loadedEnvironment.gameObject);

        if (_loadedLevelGameObjects)
            Destroy(_loadedLevelGameObjects.gameObject);

        _loadedEnvironment = Instantiate(_settings[levelIndex].LevelEnvironment, _environmentContainer.transform);
        _loadedLevelGameObjects = Instantiate(_settings[levelIndex].LevelGameObjects, _gameObjectsContainer.transform);
        _backgroundImage.sprite = _settings[levelIndex].Background;
        Camera.main.backgroundColor = _settings[levelIndex].BackgroundColor;
    }
}
