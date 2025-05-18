using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Waves[] _waves;
    [SerializeField] private LineEnemyDetector[] _lineControllers;
    public LineEnemyDetector[] LineControllers { get => _lineControllers; }

    private static WaveSpawner _instance;
    public static WaveSpawner Instance { get { return _instance; } }
    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;

    [SerializeField] private GameObject _spawnEffect;

    private void Update()
    {
        // Проверка: последняя волна + все враги уничтожены
        if (_currentWaveIndex == _waves.Length - 1 && _enemiesLeftToSpawn == 0)
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
                VictoryUIManager.Instance.ShowVictory();
                enabled = false; // отключить спавнер
            }
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        if (_waves.Length > 0)
            _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;
        LaunchWave();
    }

    private IEnumerator SpawnEnemyInWave()
    {
        if (_enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(_waves[_currentWaveIndex]
                .WaveSettings[_currentEnemyIndex]
                .SpawnDelay);

            var spawnPosition = _waves[_currentWaveIndex]
                .WaveSettings[_currentEnemyIndex].NeededSpawner.transform.position;

            Instantiate(_waves[_currentWaveIndex]
                .WaveSettings[_currentEnemyIndex].Enemy,
                spawnPosition, Quaternion.identity);

            if (_spawnEffect != null)
            {
                Instantiate(_spawnEffect, spawnPosition, Quaternion.identity);
            }

            _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex]
                .NeededSpawner.GetComponent<LineEnemyDetector>().EnemiesAlive++;

            _enemiesLeftToSpawn--;
            _currentEnemyIndex++;
            StartCoroutine(SpawnEnemyInWave());
        }
        else
        {
            if (_currentWaveIndex < _waves.Length - 1)
            {
                _currentWaveIndex++;
                _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;
                _currentEnemyIndex = 0;
            }
        }
    }


    public void LaunchWave()
    {
        StartCoroutine(SpawnEnemyInWave());
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
