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

    [SerializeField] private int startingGold = 100;
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

    private void GameOver()//����� ����
    {
        Debug.Log("Game Over!");
        //SceneManager.Pause(1);
        //SceneManager.Pause(2);
        // GameOverPanel.SetActive(true);
        // Implement game over logic here
    }
}