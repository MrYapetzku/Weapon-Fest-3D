using UnityEngine;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private EnvironmentContainer _environmentContainer;
    [SerializeField] private GameObjectsContainer _gameObjectsContainer;
    [SerializeField] private GameSoundsPlayer _soundSource;

    private LevelSettings[] _settings;
    private LevelEnvironment _loadedEnvironment;
    private LevelGameObjects _loadedLevelGameObjects;

    private LevelGameObjects _nextLoadedLevelGameObjects;

    public event UnityAction LevelGameObjectsLoaded;

    private void Awake()
    {
        _settings = Resources.LoadAll<LevelSettings>("");
        if (_settings == null)
            throw new System.Exception("Level settings resources didn't load.");
    }

    public void Load(int levelNuber)
    {
        if (levelNuber > _settings.Length)
            levelNuber = Random.Range(1, _settings.Length);

        int levelIndex = levelNuber - 1;

        if (levelIndex < 0 || levelIndex >= _settings.Length)
            throw new System.Exception("Invalid level settings index.");


        if (_loadedEnvironment)
            Destroy(_loadedEnvironment.gameObject);

        if (_loadedLevelGameObjects)
        {
            _soundSource.Release();
            Destroy(_loadedLevelGameObjects.gameObject);
        }

        RenderSettings.skybox = _settings[levelIndex].SkyBoxMaterial;
        RenderSettings.fogColor = _settings[levelIndex].FogColor;
        _loadedEnvironment = Instantiate(_settings[levelIndex].LevelEnvironment, _environmentContainer.transform);
        _loadedLevelGameObjects = Instantiate(_settings[levelIndex].LevelGameObjects, _gameObjectsContainer.transform);
        LevelGameObjectsLoaded?.Invoke();
        _soundSource.Init();
    }
}
