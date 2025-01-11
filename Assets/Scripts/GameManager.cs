using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    [Header("Settings")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    [SerializeField] private int startingGold = 100;
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private Text goldText;
    [SerializeField] private Text healthText;

    private int currentGold;
    private int currentHealth;

    public int CurrentGold => currentGold;

    private void Awake()//реализация синглтона
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameSceneObjects(Text gold, Text health) //инициализация локальных объектов
    {
        goldText = gold;
        healthText = health;
    }


    public void Play()//кнопка игры
    {
        Debug.Log("Play button clicked!");
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        SceneManager.LoadScene("GameScene");
        currentGold = startingGold;

    }

    public void QuitGame()//кнопка выхода из игры
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ReturnToMainMenu()//выход в главное меню
    {
        SceneManager.LoadScene("StartMenuScene");
    }


    public void ShowMainMenu()
    {
        Debug.Log("MenuButton clicked!");
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void ShowSettings()//кнопка настроек
    {
        Debug.Log("SettingsButton clicked!");
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    //public void ShowCredits()
    //{
    //    mainMenuPanel.SetActive(false);
    //    settingsPanel.SetActive(false);
    //    creditsPanel.SetActive(true);
    //}

    public void SetVolume(float volume)//звук
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)//экран
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }
    public void AddGold(int amount)//начисление золота
    {
        currentGold += amount;
        UpdateUI();
    }

    public bool SpendGold(int amount)//страта золота
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    public void ReduceHealth()//получение урона
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            GameOver();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (goldText != null) goldText.text = "Gold: " + currentGold;
        if (healthText != null) healthText.text = "Health: " + currentHealth;
    }

    private void GameOver()//конец игры
    {
        Debug.Log("Game Over!");
        // Implement game over logic here
    }
}