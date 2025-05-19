using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySettings : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Image _healthBarImage;

    [Header("Rewards")]
    [SerializeField] private int _resourceReward = 1;


    private WaveSpawner _waveSpawner;
    private LineEnemyDetector _lineEnemyDetector;

    private SpriteRenderer _spriteRenderer;
    private BasicEnemyWalkingState _walkingState;

    [SerializeField] private GameObject _bloodEffectPrefab;


    private bool _isSlowed = false;
    private bool _isPoisoned = false;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _waveSpawner = WaveSpawner.Instance;
        _lineEnemyDetector = _waveSpawner.LineControllers[(int)(transform.position.z / 2)];
        _healthBar.SetActive(false);

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _walkingState = GetComponent<BasicEnemyWalkingState>();
    }

    public void ReceiveDamage(int damage)
    {
        if (_currentHealth == _maxHealth)
            _healthBar.gameObject.SetActive(true);

        _currentHealth -= damage;
        _healthBarImage.fillAmount = (float)_currentHealth / _maxHealth;

        if (_currentHealth < 1)
        {
            Instantiate(_bloodEffectPrefab, transform.position, Quaternion.identity);
            ResourceCounter.Instance.ReceiveResources(_resourceReward);
            Destroy(gameObject);
        }
    }

    public void StartSlowEffect(float multiplier, float duration, Color slowColor)
    {
        StartCoroutine(ApplySlow(multiplier, duration, slowColor));
    }

    public void StartPoisonEffect(int damagePerTick, float tickInterval, float totalDuration, Color poisonColor)
    {
        StartCoroutine(ApplyPoison(damagePerTick, tickInterval, totalDuration, poisonColor));
    }

    private IEnumerator ApplySlow(float multiplier, float duration, Color slowColor)
    {
        if (_isSlowed || _walkingState == null)
            yield break;

        _isSlowed = true;
        _walkingState.SetSpeedMultiplier(multiplier);

        Color originalColor = _spriteRenderer.color;
        _spriteRenderer.color = slowColor;

        yield return new WaitForSeconds(duration);

        _walkingState.ResetSpeed();
        _spriteRenderer.color = originalColor;
        _isSlowed = false;
    }

    private IEnumerator ApplyPoison(int damagePerTick, float tickInterval, float totalDuration, Color poisonColor)
    {
        if (_isPoisoned)
            yield break;

        _isPoisoned = true;
        Color originalColor = _spriteRenderer.color;
        _spriteRenderer.color = poisonColor;

        float elapsed = 0f;
        while (elapsed < totalDuration)
        {
            ReceiveDamage(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }

        _spriteRenderer.color = originalColor;
        _isPoisoned = false;
    }

    private void OnDestroy()
    {
        if (_waveSpawner != null)
        {
            _lineEnemyDetector.EnemiesAlive--;
            // ׃האכÿול גûחמג _waveSpawner.LaunchWave();
        }
    }
}
