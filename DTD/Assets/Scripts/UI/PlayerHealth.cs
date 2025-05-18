using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [SerializeField] private int _maxHealth = 10;
    private int currentHealth;

    [SerializeField] private Image _healthBarImage;
    [SerializeField] private GameObject _deathScreen;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        currentHealth = _maxHealth;
        UpdateHealthUI();
        _deathScreen.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        _healthBarImage.fillAmount = (float)currentHealth / _maxHealth;
    }

    private void Die()
    {
        _deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
