using UnityEngine;
using UnityEngine.UI; // ��� ������ � UI

public class GameSpeedController : MonoBehaviour
{
    public Button speedUpButton;  // ������ �� ������ ���������
    private bool isSpeedUp = false;  // ���������� ��� ������������ ��������� ���������

    void Start()
    {
        if (speedUpButton != null)
        {
            speedUpButton.onClick.AddListener(ToggleGameSpeed);
        }
    }

    // ����� ��� ������������ �������� ����
    void ToggleGameSpeed()
    {
        if (isSpeedUp)
        {
            // ������������� ���������� �������� ����
            Time.timeScale = 1f;
            isSpeedUp = false;
        }
        else
        {
            // ������������� ���������� �������� ����
            Time.timeScale = 2f; // ��������� � 2 ����
            isSpeedUp = true;
        }
    }
}

