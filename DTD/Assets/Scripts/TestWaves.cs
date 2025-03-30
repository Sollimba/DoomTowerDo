using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaves : MonoBehaviour
{
    private WaveSpawner _waveSpawner;
    private LineController _lineController;

    private void Awake()
    {
        _waveSpawner = WaveSpawner.Instance;
        _lineController = _waveSpawner.LineControllers[(int)(transform.position.z / 2)];
    }

    private void OnDestroy()
    {
        if (_waveSpawner != null)
        {
            int enemiesLeft = 0;
            enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemiesLeft == 0)
                _waveSpawner.LaunchWave();
            _lineController.EnemiesAlive--;
        }
    }
}
