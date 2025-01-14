using UnityEngine;
using UnityEngine.UI; // Для работы с UI

public class GameSpeedController : MonoBehaviour
{
    public Button speedUpButton;  // Ссылка на кнопку ускорения
    private bool isSpeedUp = false;  // Переменная для отслеживания состояния ускорения

    void Start()
    {
        if (speedUpButton != null)
        {
            speedUpButton.onClick.AddListener(ToggleGameSpeed);
        }
    }

    // Метод для переключения скорости игры
    void ToggleGameSpeed()
    {
        if (isSpeedUp)
        {
            // Устанавливаем нормальную скорость игры
            Time.timeScale = 1f;
            isSpeedUp = false;
        }
        else
        {
            // Устанавливаем ускоренную скорость игры
            Time.timeScale = 2f; // Ускорение в 2 раза
            isSpeedUp = true;
        }
    }
}

