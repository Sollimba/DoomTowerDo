using UnityEngine;

public class EnemySettings : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    private WaveSpawner _waveSpawner;
    private LineEnemyDetector _lineEnemyDetector;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _waveSpawner = WaveSpawner.Instance;
        _lineEnemyDetector = _waveSpawner.LineControllers[(int)(transform.position.z / 2)];
    }

    public void ReceiveDamage (int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <1)
            Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (_waveSpawner != null)
        {
            _lineEnemyDetector.EnemiesAlive--;
            int enemiesLeft = 0;
            enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemiesLeft == 0)
                _waveSpawner.LaunchWave();
        }
    }
}
