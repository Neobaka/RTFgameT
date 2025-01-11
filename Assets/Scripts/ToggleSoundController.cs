using UnityEngine;
using UnityEngine.UI; // ��� ������ � UI ������������

public class ToggleSoundController : MonoBehaviour
{
    public Toggle soundToggle; // ������ �� Toggle � UI

    private void Start()
    {
        // ���������, ���� �� ������ �� Toggle
        if (soundToggle != null)
        {
            // ��������� ��������� �� ������� ��������� ��������� Toggle
            soundToggle.onValueChanged.AddListener(OnToggleChanged);

            // ������������� ��������� ��������� �� ���������� ��� ��������� ��������
            bool isSoundOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1; // 1 - ���� �������, 0 - ��������
            soundToggle.isOn = isSoundOn;
            UpdateSoundState(isSoundOn);
        }
    }

    // �����, ���������� ��� ��������� ��������� Toggle
    private void OnToggleChanged(bool isOn)
    {
        Debug.Log("Sound Toggle State: " + (isOn ? "ON" : "OFF"));

        // ��������� ��������� �����
        UpdateSoundState(isOn);

        // ��������� ��������� �����
        PlayerPrefs.SetInt("SoundEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    // ����� ��� ���������/���������� �����
    private void UpdateSoundState(bool isOn)
    {
        AudioListener.volume = isOn ? 1.0f : 0.0f; // 1.0 - ��������� ��������, 0.0 - ���������
    }

    private void OnDestroy()
    {
        // ������� ���������, ����� �������� ������
        if (soundToggle != null)
        {
            soundToggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
    }
}

