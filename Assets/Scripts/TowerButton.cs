using UnityEngine;
using UnityEngine.UI;
using TMPro; // Добавляем для TextMeshProUGUI

public class TowerButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI nameText;
    private TowerManager.TowerOption towerOption;

    private void Awake()
    {
        ValidateComponents();
    }

    private void ValidateComponents()
    {
        // Если компоненты не назначены через инспектор, пытаемся найти их
        if (iconImage == null) iconImage = GetComponentInChildren<Image>();
        if (costText == null) costText = transform.Find("Cost")?.GetComponent<TextMeshProUGUI>();
        if (nameText == null) nameText = transform.Find("Name")?.GetComponent<TextMeshProUGUI>();

        // Логируем ошибки, если что-то не найдено
        if (iconImage == null) Debug.LogError($"Icon Image not found on {gameObject.name}", this);
        if (costText == null) Debug.LogError($"Cost Text not found on {gameObject.name}", this);
        if (nameText == null) Debug.LogError($"Name Text not found on {gameObject.name}", this);
    }

    public void Initialize(TowerManager.TowerOption tower)
    {
        if (tower == null)
        {
            Debug.LogError($"Attempting to initialize {gameObject.name} with null tower option", this);
            return;
        }

        Debug.Log($"Initializing button with tower: {tower.towerName}");
        Debug.Log($"Tower prefab: {(tower.towerPrefab != null ? tower.towerPrefab.name : "null")}");
        Debug.Log($"Tower cost: {tower.cost}");
        Debug.Log($"Tower icon: {(tower.icon != null ? tower.icon.name : "null")}");

        towerOption = tower;

        if (iconImage == null)
        {
            Debug.LogError($"Icon Image component is missing on {gameObject.name}", this);
            return;
        }

        if (costText == null)
        {
            Debug.LogError($"Cost Text component is missing on {gameObject.name}", this);
            return;
        }

        if (nameText == null)
        {
            Debug.LogError($"Name Text component is missing on {gameObject.name}", this);
            return;
        }

        if (tower.icon != null)
        {
            iconImage.sprite = tower.icon;
        }
        else
        {
            Debug.LogWarning($"No icon specified for tower {tower.towerName}");
        }

        costText.text = tower.cost.ToString();
        nameText.text = tower.towerName;

        Debug.Log($"TowerButton {gameObject.name} initialization completed");
    }

    public void OnClick()
    {
        Debug.Log($"Button clicked. TowerOption: {(towerOption != null ? towerOption.towerName : "null")}");

        if (towerOption == null)
        {
            Debug.LogError($"TowerOption is null in {gameObject.name}", this);
            return;
        }

        if (towerOption.towerPrefab == null)
        {
            Debug.LogError($"Tower prefab is null for {towerOption.towerName}", this);
            return;
        }

        if (TowerManager.Instance == null)
        {
            Debug.LogError("TowerManager instance is null", this);
            return;
        }

        Debug.Log($"Selecting tower: {towerOption.towerName}");
        TowerManager.Instance.SelectTower(towerOption);
    }
}