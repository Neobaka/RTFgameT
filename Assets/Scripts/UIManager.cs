using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject gamePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;

    [Header("Game UI")]
    public Text waveText;
    public Text goldText;
    public Text healthText;
    public Text scoreText;

    [Header("Tower UI")]
    public GameObject towerUpgradePanel;
    public Text towerStatsText;
    public Button upgradeButton;

    private Tower selectedTower;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowTowerStats(Tower tower)
    {
        selectedTower = tower;
        towerUpgradePanel.SetActive(true);
        UpdateTowerUI();
    }

    public void HideTowerStats()
    {
        selectedTower = null;
        towerUpgradePanel.SetActive(false);
    }

    public void UpgradeSelectedTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Upgrade();
            UpdateTowerUI();
        }
    }

    private void UpdateTowerUI()
    {
        if (selectedTower != null)
        {
            towerStatsText.text = $"Damage: {selectedTower.Damage}\n" +
                                 $"Range: {selectedTower.Range}\n" +
                                 $"Fire Rate: {selectedTower.FireRate}";
        }
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}