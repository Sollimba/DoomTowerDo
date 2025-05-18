using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;

    [Header("Sound Toggles")]
    [SerializeField] private Button musicToggleButton;
    [SerializeField] private Button sfxToggleButton;

    private bool isMusicOn = true;
    private bool isSFXOn = true;

    private void Awake()
    {
        // Загрузка сохранённых значений
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        isSFXOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
    }

    private void Start()
    {
        UpdateSoundButtons();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        UpdateSoundButtons();
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        UpdateSoundButtons();
        // AudioManager.Instance.SetMusicEnabled(isMusicOn);
    }

    public void ToggleSFX()
    {
        isSFXOn = !isSFXOn;
        PlayerPrefs.SetInt("SFXOn", isSFXOn ? 1 : 0);
        UpdateSoundButtons();
        // AudioManager.Instance.SetSFXEnabled(isSFXOn);
    }

    private void UpdateSoundButtons()
    {
        if (musicToggleButton != null)
            musicToggleButton.GetComponentInChildren<Text>().text = isMusicOn ? "Музыка: ВКЛ" : "Музыка: ВЫКЛ";

        if (sfxToggleButton != null)
            sfxToggleButton.GetComponentInChildren<Text>().text = isSFXOn ? "Звуки: ВКЛ" : "Звуки: ВЫКЛ";
    }

    public bool IsMusicOn => isMusicOn;
    public bool IsSFXOn => isSFXOn;
}
