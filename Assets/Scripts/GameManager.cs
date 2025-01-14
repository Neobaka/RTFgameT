using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] private Text goldText;
    [SerializeField] private Text healthText;

    private int currentGold;
    private Tower mainTower; // ������ �� �������� ����� (MainTower)

    public int CurrentGold => currentGold;

    private void Awake()//���������� ���������
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

    public void ReturnToMainMenu()//����� � ������� ����
    {
        SceneManager.LoadScene("StartMenuScene");
    }

    public void Play()//������ ����
    {
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

    public void PlayEducation()//������ ����
    {
        Debug.Log("PlayEducation button clicked!");
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        //GameOverPanel.SetActive(false);
        SceneManager.LoadScene("EducationScene");
        //currentGold = startingGold;

    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        Time.timeScale = 0f; // ������������� �����
        GameOverPanel.SetActive(true); // ���������� ������ ���������� ����
    }

    public void SetGameSceneObjects(Text gold, Text health, Tower mainTower) //������������� ��������� ��������
    {
        goldText = gold;
        healthText = health;
        this.mainTower = mainTower; // ������������� ������ �� MainTower
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
        // ���� �������� �����������, �������� ���������� HP ���� ��� MainTower
        if (mainTower != null)
        {
            mainTower.UpdateHpBar();
        }

        // ������ ��������� ����� (��������, ���������� �������� ������)
        // ����� ����� �������� ������, ������� ����� ��������� �������� ������ � ��������� UI
    }

    private void UpdateUI()
    {
        if (goldText != null) goldText.text = "Gold: " + currentGold;
        if (healthText != null) healthText.text = "Health: " + currentGold;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // ������������� �����
        Debug.Log("Game paused.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // ���������� ����� � ���������� ���
        Debug.Log("Game resumed.");
    }

    public void GameOver()//����� ����
    {
        Debug.Log("Game Over!");
        PauseGame();

        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true); // ���������� ����� "Game Over"
        }
    }
}