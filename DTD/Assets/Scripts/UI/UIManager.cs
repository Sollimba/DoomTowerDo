using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Health Settings")]
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private Image _healthBarImage;

    private int _currentHealth;

    [Header("Panels")]
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private GameObject _pausePanel;

    private bool _isPaused = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _currentHealth = _maxHealth;

        _victoryPanel.SetActive(false);
        _deathPanel.SetActive(false);
        _pausePanel.SetActive(false);

        UpdateHealthUI();
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    //HEALTH
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        UpdateHealthUI();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (_healthBarImage != null)
            _healthBarImage.fillAmount = (float)_currentHealth / _maxHealth;
    }

    private void Die()
    {
        _deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    //VICTORY
    public void ShowVictory()
    {
        _victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    //COMMON ACTIONS
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    //PAUSE
    public void TogglePause()
    {
        _isPaused = !_isPaused;
        _pausePanel.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        _isPaused = false;
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
