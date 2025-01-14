using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject GameOverPanel;
    public GameObject creditsPanel;

    [Header("Settings")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    [Header("UI")]
    public TextMeshProUGUI waveCountAtEndText;


    [SerializeField] private Text goldText;
    [SerializeField] private Text healthText;

    private int currentGold;
    private Tower mainTower; // Ссылка на основную башню (MainTower)

    public int CurrentGold => currentGold;
    //public int CurrentWaveIndex => currentWaveIndex;


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

    public void ReturnToMainMenu()//выход в главное меню
    {
        SceneManager.LoadScene("StartMenuScene");
    }

    public void Play()//кнопка игры
    {
        Time.timeScale = 1f;
        Debug.Log("Play button clicked!");
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        //GameOverPanel.SetActive(false);
        //if (educationBeen)
        //    SceneManager.LoadScene("1");
        //else
        //    SceneManager.LoadScene("EducationScene");
        SceneManager.LoadScene("GameScene");
        //currentGold = startingGold;
    }

    public void PlayEducation()//кнопка игры
    {
        Time.timeScale = 1f;
        Debug.Log("PlayEducation button clicked!");
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        //GameOverPanel.SetActive(false);
        SceneManager.LoadScene("EducationScene");
        //currentGold = startingGold;

    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        Time.timeScale = 0f; // Останавливает время
        GameOverPanel.SetActive(true); // Показывает панель завершения игры
    }

    public void SetGameSceneObjects(Text gold, Text health, Tower mainTower) //инициализация локальных объектов
    {
        goldText = gold;
        healthText = health;
        this.mainTower = mainTower; // Устанавливаем ссылку на MainTower
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateUI();
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    public void ReduceHealth()
    {
        // Если здоровье уменьшилось, вызываем обновление HP бара для MainTower
        if (mainTower != null)
        {
            mainTower.UpdateHpBar();
        }

        // Логика получения урона (например, уменьшение здоровья игрока)
        // Здесь можно добавить логику, которая будет уменьшать здоровье игрока и обновлять UI
    }

    private void UpdateUI()
    {
        if (goldText != null) goldText.text = "Gold: " + currentGold;
        if (healthText != null) healthText.text = "Health: " + currentGold;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Останавливает время
        Debug.Log("Game paused.");
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f; // Останавливает время
        Debug.Log("Game unpaused.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Возвращает время в нормальный ход
        Debug.Log("Game resumed.");
    }

    public void GameOver()//конец игры
    {
        Debug.Log("Game Over!");
        PauseGame();

        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true); // Отображаем экран "Game Over"
            waveCountAtEndText.text = $"Количество пройденных волн {WaveManager.Instance.CurrentWaveIndex}!";
        }
    }
}