using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Waves[] _waves;
    [SerializeField] private LineEnemyDetector[] _lineControllers;
    public LineEnemyDetector[] LineControllers => _lineControllers;

    private static WaveSpawner _instance;
    public static WaveSpawner Instance => _instance;

    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;
    private bool _waveInProgress = false;
    private bool _waitingForNextWave = false;

    [SerializeField] private GameObject _spawnEffect;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        if (_waves.Length > 0)
        {
            _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;
            StartCoroutine(SpawnEnemyInWave());
            _waveInProgress = true;
        }
    }

    private void Update()
    {
        if (_waveInProgress && _enemiesLeftToSpawn == 0)
        {
            bool allClear = true;
            foreach (var line in _lineControllers)
            {
                if (line.EnemiesAlive > 0)
                {
                    allClear = false;
                    break;
                }
            }

            if (allClear)
            {
                _waveInProgress = false;

                if (_currentWaveIndex == _waves.Length - 1)
                {
                    UIManager.Instance.ShowVictory();
                    enabled = false;
                }
                else if (!_waitingForNextWave)
                {
                    _waitingForNextWave = true;
                    StartCoroutine(StartNextWaveAfterDelay());
                }
            }
        }
    }

    private IEnumerator SpawnEnemyInWave()
    {
        while (_enemiesLeftToSpawn > 0 && _currentEnemyIndex < _waves[_currentWaveIndex].WaveSettings.Length)
        {
            var waveSetting = _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex];

            yield return new WaitForSeconds(waveSetting.SpawnDelay);

            Transform spawnerTransform = waveSetting.NeededSpawner.transform;
            Vector3 spawnPosition = spawnerTransform.position;

            if (_spawnEffect != null)
            {
                Quaternion spawnRotation = Quaternion.Euler(-90f, 0f, 0f);
                Instantiate(_spawnEffect, spawnPosition, spawnRotation);
            }

            yield return new WaitForSeconds(1f);

            Instantiate(waveSetting.Enemy, spawnPosition, Quaternion.identity);
            waveSetting.NeededSpawner.GetComponent<LineEnemyDetector>().EnemiesAlive++;

            _enemiesLeftToSpawn--;
            _currentEnemyIndex++;
        }
    }

    private IEnumerator StartNextWaveAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Ќебольша€ пауза перед новой волной (можно убрать)

        _currentWaveIndex++;
        _currentEnemyIndex = 0;
        _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;

        StartCoroutine(SpawnEnemyInWave());
        _waveInProgress = true;
        _waitingForNextWave = false;
    }
}


[System.Serializable]
public class Waves
{
    [SerializeField] private WaveSettings[] _waveSettings;
    public WaveSettings[] WaveSettings { get => _waveSettings; }
}

[System.Serializable]
public class WaveSettings
{
    [SerializeField] private GameObject _enemy;
    public GameObject Enemy { get => _enemy; }
    [SerializeField] private GameObject _neededSpawner;
    public GameObject NeededSpawner { get => _neededSpawner; }
    [SerializeField] private float _spawnDelay;
    public float SpawnDelay { get => _spawnDelay; }
}
