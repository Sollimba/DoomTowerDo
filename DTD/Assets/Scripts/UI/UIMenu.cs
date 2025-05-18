using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using SmallHedge.SoundManager;

public class UIMenu : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;

    [Header("Sound Toggles")]
    [SerializeField] private Button musicToggleButton;

    private bool isMusicOn = true;

    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        // Загрузка сохранённых значений
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
    }

    private void Start()
    {
        UpdateSoundButtons();

        if (isMusicOn && musicSource != null && !musicSource.isPlaying)
        {
            SoundManager.PlaySound(SoundType.BackgroundMusicManu, musicSource);
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("lvl_1");
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
        PlayerPrefs.Save();

        if (isMusicOn)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }

        UpdateSoundButtons();
    }


    private void UpdateSoundButtons()
    {
        if (musicToggleButton != null)
            musicToggleButton.GetComponentInChildren<TMP_Text>().text = isMusicOn ? "Музыка: ВКЛ" : "Музыка: ВЫКЛ";

    }

    public bool IsMusicOn => isMusicOn;

}
