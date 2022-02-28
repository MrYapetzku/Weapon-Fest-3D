using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
    private const string LEVEL_SETTINGS_PATH = "";

    [SerializeField] private EnvironmentContainer _environmentContainer;
    [SerializeField] private GameObjectsContainer _gameObjectsContainer;
    [SerializeField] private GameSoundsPlayer _soundSource;

    private int _currentLevelIndex;
    private int _nextLevelIndex;
    private AsyncOperationHandle<GameObject> _currentLevelResult;
    private AsyncOperationHandle<GameObject> _nextLevelResult;
    private bool _isFirstRun;

    private LevelSettings[] _settings;
    private LevelEnvironment _loadedEnvironment;

    public event UnityAction LevelLoaded;

    private void Awake()
    {
        //Fader.Instance.gameObject.SetActive(true);
        _settings = Resources.LoadAll<LevelSettings>(LEVEL_SETTINGS_PATH);
        if (_settings == null)
            throw new System.Exception("Level settings resources didn't load.");

        _isFirstRun = true;
    }

    public async void Load(int levelNumber)
    {
        if (_isFirstRun)
        {
            _nextLevelIndex = GetValidLevelIndex(levelNumber);
            _nextLevelResult = Addressables.InstantiateAsync(_settings[_nextLevelIndex].LevelGameObjects, _gameObjectsContainer.transform);
        }

        _currentLevelIndex = _nextLevelIndex;
        _nextLevelIndex = GetValidLevelIndex(levelNumber + 1);

        if (_loadedEnvironment)
            Destroy(_loadedEnvironment.gameObject);

        if (!_isFirstRun)
        {
            _soundSource.Release();
            _currentLevelResult.Result.gameObject.SetActive(false);
            Addressables.ReleaseInstance(_currentLevelResult.Result);
        }

        RenderSettings.skybox = _settings[_currentLevelIndex].SkyBoxMaterial;
        RenderSettings.fogColor = _settings[_currentLevelIndex].FogColor;
        _loadedEnvironment = Instantiate(_settings[_currentLevelIndex].LevelEnvironment, _environmentContainer.transform);

        _currentLevelResult = _nextLevelResult;

        await _currentLevelResult.Task;
        if (_currentLevelResult.Status == AsyncOperationStatus.Succeeded)
        {
            _currentLevelResult.Result.gameObject.SetActive(true);
            _isFirstRun = false;

            LevelLoaded?.Invoke();

            _soundSource.Init();

            _nextLevelResult = Addressables.InstantiateAsync(_settings[_nextLevelIndex].LevelGameObjects, _gameObjectsContainer.transform);
            await _nextLevelResult.Task;
            if (_nextLevelResult.Status == AsyncOperationStatus.Succeeded)
                _nextLevelResult.Result.gameObject.SetActive(false);
        }
    }

    private int GetValidLevelIndex(int levelNumber)
    {
        if (levelNumber < 1)
            throw new System.Exception("Invalid level number.");

        if (levelNumber > _settings.Length)
            return Random.Range(0, _settings.Length - 1);
        else
            return levelNumber - 1;
    }
}
