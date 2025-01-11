using UnityEngine;
using UnityEngine.UI; // Для работы с UI компонентами

public class ToggleSoundController : MonoBehaviour
{
    public Toggle soundToggle; // Ссылка на Toggle в UI

    private void Start()
    {
        // Проверяем, есть ли ссылка на Toggle
        if (soundToggle != null)
        {
            // Добавляем слушатель на событие изменения состояния Toggle
            soundToggle.onValueChanged.AddListener(OnToggleChanged);

            // Устанавливаем начальное состояние из сохранений или дефолтное значение
            bool isSoundOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1; // 1 - звук включен, 0 - выключен
            soundToggle.isOn = isSoundOn;
            UpdateSoundState(isSoundOn);
        }
    }

    // Метод, вызываемый при изменении состояния Toggle
    private void OnToggleChanged(bool isOn)
    {
        Debug.Log("Sound Toggle State: " + (isOn ? "ON" : "OFF"));

        // Обновляем состояние звука
        UpdateSoundState(isOn);

        // Сохраняем состояние звука
        PlayerPrefs.SetInt("SoundEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Метод для включения/выключения звука
    private void UpdateSoundState(bool isOn)
    {
        AudioListener.volume = isOn ? 1.0f : 0.0f; // 1.0 - громкость включена, 0.0 - выключена
    }

    private void OnDestroy()
    {
        // Удаляем слушатель, чтобы избежать ошибок
        if (soundToggle != null)
        {
            soundToggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
    }
}

