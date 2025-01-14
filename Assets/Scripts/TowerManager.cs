//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.Events;
//using UnityEngine.EventSystems;
//using System.Collections.Generic;

//public class TowerManager : MonoBehaviour
//{
//    public static TowerManager Instance { get; private set; }
//    [System.Serializable]
//    public class TowerOption
//    {
//        public GameObject towerPrefab;
//        public int cost;
//        public Sprite icon;
//        public string towerName;
//    }

//    //private List<TowerButton> spawnedButtons = new List<TowerButton>();
//    public TowerOption selectedTower; // Добавляем переменную для хранения выбранной башни
//    //private Camera mainCamera;
//    //private bool isPlacingTower;
//    [SerializeField] private LayerMask towerSpotLayer; // Слой для определения точек размещения башен



//    [Header("Tower Settings")]
//    public TowerOption[] availableTowers;
//    public LayerMask placementArea;
//    public GameObject towerSpotPrefab; // Префаб места для башни
//    public Material validPlacementMaterial;
//    public Material invalidPlacementMaterial;

//    [Header("UI Elements")]
//    public GameObject towerButtonPrefab;
//    public Transform towerButtonsParent;
//    public TextMeshProUGUI goldText;
//    public TextMeshProUGUI tooltipText; // Для отображения подсказок

//    //private TowerOption selectedTower;
//    private int gold = 300;
//    private GameObject previewTower; // Для предварительного просмотра башни
//    private TowerSpot highlightedSpot; // Текущее подсвеченное место для башни

//    void Awake()
//    {
//        // Проверка синглтона
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//            Debug.Log("TowerManager initialized");
//        }
//        else
//        {
//            Destroy(gameObject);
//            return;
//        }

//        // Проверка необходимых компонентов
//        if (availableTowers == null || availableTowers.Length == 0)
//        {
//            Debug.LogError("No towers configured in TowerManager!");
//            return;
//        }

//        if (towerButtonPrefab == null)
//        {
//            Debug.LogError("Tower button prefab is not assigned!");
//            return;
//        }

//        if (towerButtonsParent == null)
//        {
//            Debug.LogError("Tower buttons parent is not assigned!");
//            return;
//        }
//        ValidateTowerPrefabs();
//        //CreateTowerButtons();
//    }

//    private void ValidateTowerPrefabs()
//    {
//        if (availableTowers == null || availableTowers.Length == 0)
//        {
//            Debug.LogError("No towers configured in TowerManager!");
//            return;
//        }

//        for (int i = 0; i < availableTowers.Length; i++)
//        {
//            if (availableTowers[i] == null)
//            {
//                Debug.LogError($"Tower option at index {i} is null!");
//                continue;
//            }

//            if (availableTowers[i].towerPrefab == null)
//            {
//                Debug.LogError($"Tower prefab is missing for tower: {availableTowers[i].towerName}");
//            }
//        }
//    }


//    //void CreateTowerButtons()
//    //{
//    //    foreach (var tower in availableTowers)
//    //    {
//    //        if (tower.towerPrefab == null)
//    //        {
//    //            Debug.LogError($"Tower prefab is missing for tower: {tower.towerName}");
//    //            continue;
//    //        }

//    //        GameObject buttonObj = Instantiate(towerButtonPrefab, towerButtonsParent);
//    //        TowerButton towerButton = buttonObj.GetComponent<TowerButton>();

//    //        if (towerButton == null)
//    //        {
//    //            Debug.LogError("TowerButton component not found on instantiated button!");
//    //            continue;
//    //        }

//    //        Debug.Log($"Initializing button for tower: {tower.towerName}");
//    //        towerButton.Initialize(tower);
//    //    }

//    //    //// Настройка иконки и текста
//    //    //buttonObj.transform.Find("Icon").GetComponent<Image>().sprite = tower.icon;
//    //    //buttonObj.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = tower.cost.ToString();
//    //    //buttonObj.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = tower.towerName;



//    //    //// Добавляем обработчики наведения для подсказок
//    //    //button.onClick.AddListener(() => SelectTower(tower));

//    //    //// Добавляем всплывающие подсказки
//    //    //EventTrigger trigger = buttonObj.AddComponent<EventTrigger>();

//    //    //EventTrigger.Entry enterEntry = new EventTrigger.Entry();
//    //    //enterEntry.eventID = EventTriggerType.PointerEnter;
//    //    //enterEntry.callback.AddListener((data) => { ShowTowerTooltip(tower); });
//    //    //trigger.triggers.Add(enterEntry);

//    //    //EventTrigger.Entry exitEntry = new EventTrigger.Entry();
//    //    //exitEntry.eventID = EventTriggerType.PointerExit;
//    //    //exitEntry.callback.AddListener((data) => { HideTooltip(); });
//    //    //trigger.triggers.Add(exitEntry);


//    //    UpdateGoldText();
//    //}

//    void Update()
//    {
//        if (selectedTower != null)
//        {
//            //UpdateTowerPreview();

//            if (Input.GetMouseButtonDown(0))
//            {
//                TryPlaceTower();
//            }
//            else if (Input.GetMouseButtonDown(1))
//            {
//                CancelTowerPlacement();
//            }
//        }

//    }

//    //public void TryPlaceTower()
//    //{
//    //    if (selectedTower == null || selectedTower.towerPrefab == null)
//    //    {
//    //        Debug.LogError("No valid tower selected for placement!");
//    //        return;
//    //    }

//    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//    //    RaycastHit hit;

//    //    if (Physics.Raycast(ray, out hit, 100f, towerSpotLayer))
//    //    {
//    //        TowerSpot spot = hit.collider.GetComponent<TowerSpot>();
//    //        if (spot != null)
//    //        {
//    //            if (!spot.IsOccupied)
//    //            {
//    //                GameObject tower = Instantiate(selectedTower.towerPrefab, spot.transform.position, Quaternion.identity);
//    //                spot.SetOccupied(true);
//    //                Debug.Log("Tower placed successfully");
//    //                // Опционально: очистить выбор после размещения
//    //                ClearSelection();
//    //            }
//    //            else
//    //            {
//    //                Debug.Log("Spot is occupied");
//    //            }
//    //        }
//    //    }
//    //}
//    public void TryPlaceTower()
//    {
//        if (selectedTower == null || selectedTower.towerPrefab == null)
//        {
//            Debug.LogError("No valid tower selected for placement!");
//            return;
//        }

//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 5f);
//        RaycastHit hit;

//        if (Physics.Raycast(ray, out hit, 100f, towerSpotLayer))
//        {
//            Debug.Log($"Hit: {hit.collider.name}");
//            TowerSpot spot = hit.collider.GetComponent<TowerSpot>();
//            if (spot != null)
//            {
//                if (!spot.IsOccupied)
//                {
//                    GameObject tower = Instantiate(selectedTower.towerPrefab, spot.transform.position, Quaternion.identity);
//                    spot.SetOccupied(true);
//                    Debug.Log("Tower placed successfully");
//                    // Опционально: очистить выбор после размещения
//                    ClearSelection();
//                }
//                else
//                {
//                    Debug.Log("Spot is occupied");
//                }
//            }
//        }
//    }


//    //void PlaceTower(Vector3 position)
//    //{
//    //    GameObject tower = Instantiate(selectedTower.towerPrefab, position, Quaternion.identity);
//    //    // Можно добавить дополнительную инициализацию башни здесь
//    //}

//    public void SelectTower(TowerOption tower)
//    {
//        if (tower == null)
//        {
//            Debug.LogError("Attempting to select null tower!");
//            return;
//        }

//        if (tower.towerPrefab == null)
//        {
//            Debug.LogError($"Tower prefab is missing for {tower.towerName}!");
//            return;
//        }

//        selectedTower = tower;
//        //isPlacingTower = true;
//        Debug.Log($"Selected tower: {tower.towerName}");
//    }

//    //void CreateTowerPreview()
//    //{
//    //    if (previewTower != null)
//    //        Destroy(previewTower);

//    //    previewTower = Instantiate(selectedTower.towerPrefab);
//    //    // Сделать превью полупрозрачным
//    //    SetPreviewTransparency(previewTower, 0.5f);
//    //}

//    //void SetPreviewTransparency(GameObject preview, float alpha)
//    //{
//    //    Renderer[] renderers = preview.GetComponentsInChildren<Renderer>();
//    //    foreach (Renderer renderer in renderers)
//    //    {
//    //        Material[] materials = renderer.materials;
//    //        foreach (Material material in materials)
//    //        {
//    //            Color color = material.color;
//    //            color.a = alpha;
//    //            material.color = color;
//    //        }
//    //    }
//    //}

//    void CancelTowerPlacement()
//    {
//        if (previewTower != null)
//            Destroy(previewTower);

//        selectedTower = null;
//        ShowPlacementGuide(false);
//    }

//    void ShowPlacementGuide(bool show)
//    {
//        // Показать/скрыть подсказку по размещению
//        if (tooltipText != null)
//            tooltipText.text = show ? "Click to place tower. Right click to cancel." : "";
//    }

//    void ShowTowerTooltip(TowerOption tower)
//    {
//        if (tooltipText != null)
//            tooltipText.text = $"{tower.towerName}\nCost: {tower.cost} gold";
//    }

//    void HideTooltip()
//    {
//        if (tooltipText != null)
//            tooltipText.text = "";
//    }

//    void ShowNotEnoughGoldMessage()
//    {
//        if (tooltipText != null)
//            tooltipText.text = "Not enough gold!";
//        //StartCoroutine(ClearTooltipAfterDelay(2f));
//    }

//    //IEnumerator ClearTooltipAfterDelay(float delay)
//    //{
//    //    yield return new WaitForSeconds(delay);
//    //    HideTooltip();
//    //}

//    public void ShowPreview(Vector3 position)
//    {
//        if (previewTower != null)
//        {
//            previewTower.SetActive(true);
//            previewTower.transform.position = position;
//        }
//    }

//    public void HidePreview()
//    {
//        if (previewTower != null)
//        {
//            previewTower.SetActive(false);
//        }
//    }


//    void HighlightSpot(TowerSpot spot, bool isValid)
//    {
//        Renderer renderer = spot.GetComponent<Renderer>();
//        if (renderer != null)
//        {
//            renderer.material = isValid ? validPlacementMaterial : invalidPlacementMaterial;
//        }
//    }



//    bool CanAfford(TowerOption tower)
//    {
//        return gold >= tower.cost;
//    }

//    public void AddGold(int amount)
//    {
//        gold += amount;
//        UpdateGoldText();
//    }

//    void UpdateGoldText()
//    {
//        if (goldText != null)
//            goldText.text = $"Gold: {gold}";
//    }

//    private void CancelPlacement()
//    {
//        //isPlacingTower = false;
//        selectedTower = null;
//        Debug.Log("Tower placement cancelled");
//    }

//    public void ClearSelection()
//    {
//        selectedTower = null;
//        //isPlacingTower = false;
//    }

//    public bool HasSelectedTower()
//    {
//        return selectedTower != null && selectedTower.towerPrefab != null;
//    }
//}
//using System.Collections.Generic;
//using UnityEngine;

//public class TowerManager : MonoBehaviour
//{
//    public static TowerManager Instance { get; private set; }

//    [System.Serializable]
//    public class TowerOption
//    {
//        public string towerName;
//        public int cost;
//        public GameObject towerPrefab;      
//        public Sprite icon;
//    }

//    [SerializeField] private List<TowerOption> towerOptions;
//    public List<TowerOption> TowerOptions => towerOptions;
//    [SerializeField] private int playerMoney = 100;
//    [SerializeField] private List<GameObject> previewTower;

//    private TowerOption selectedTower;

//    private void Awake()
//    {
//        if (Instance != null && Instance != this)
//        {
//            Destroy(gameObject);
//            return;
//        }
//        Instance = this;
//    }

//    public void SelectTower(int index)
//    {
//        if (index < 0 || index >= towerOptions.Count)
//        {
//            Debug.LogError("Invalid tower index!");
//            return;
//        }

//        selectedTower = towerOptions[index];
//        Debug.Log($"Selected tower: {selectedTower.towerName}");
//    }

//    public bool HasSelectedTower()
//    {
//        return selectedTower != null;
//    }

//    public bool CanAffordSelectedTower()
//    {
//        return selectedTower != null && playerMoney >= selectedTower.cost;
//    }

//    public void SpendMoney(int amount)
//    {
//        playerMoney -= amount;
//        Debug.Log($"Player money: {playerMoney}");
//    }

//    public void RefundMoney(int amount)
//    {
//        playerMoney += amount;
//        Debug.Log($"Player money refunded: {playerMoney}");
//    }

//    public void TryPlaceTower(Vector3 position, TowerSpot spot)
//    {
//        if (selectedTower == null || spot.IsOccupied || !CanAffordSelectedTower())
//        {
//            Debug.LogWarning("Cannot place tower!");
//            return;
//        }

//        GameObject tower = Instantiate(selectedTower.towerPrefab, position, Quaternion.identity);
//        spot.SetOccupied(true, tower);
//        SpendMoney(selectedTower.cost);

//        Debug.Log($"Placed {selectedTower.towerName} at {position}");
//    }

//    //public void ShowPreview(Vector3 position)
//    //{
//    //    if (previewTower != null)
//    //    {
//    //        previewTower.SetActive(true);
//    //        previewTower.transform.position = position;
//    //    }
//    //}

//    //public void HidePreview()
//    //{
//    //    if (previewTower != null)
//    //    {
//    //        previewTower.SetActive(false);
//    //    }
//    //}

//    public void RemoveTower(TowerSpot spot)
//    {
//        if (!spot.IsOccupied)
//        {
//            Debug.LogWarning("No tower to remove!");
//            return;
//        }

//        GameObject tower = spot.GetPlacedTower();
//        if (tower != null)
//        {
//            Destroy(tower);
//            RefundMoney(spot.GetRefundAmount());
//            spot.SetOccupied(false, null);

//            Debug.Log("Tower removed and money refunded.");
//        }
//    }
//}
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    [System.Serializable]
    public class TowerOption
    {
        public string towerName;
        public GameObject towerPrefab;
        public int cost;
    }

    [SerializeField] private List<TowerOption> towerOptions;
    [SerializeField] private int playerMoney = 100;
    [SerializeField] private TMPro.TextMeshProUGUI goldText; // UI для отображения золота


    public List<TowerOption> TowerOptions => towerOptions;

    private TowerOption selectedTower;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateGoldUI();
    }

    private void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = $"{playerMoney}";
        }
    }

    public void SelectTower(int index)
    {
        if (index < 0 || index >= towerOptions.Count)
        {
            Debug.LogError("Invalid tower index!");
            return;
        }

        selectedTower = towerOptions[index];
        Debug.Log($"Selected tower: {selectedTower.towerName}");
    }

    public bool HasSelectedTower()
    {
        return selectedTower != null;
    }

    public bool CanAffordSelectedTower()
    {
        return selectedTower != null && playerMoney >= selectedTower.cost;
    }

    public void SpendMoney(int amount)
    {
        playerMoney -= amount;
        Debug.Log($"Player money: {playerMoney}");
        UpdateGoldUI();
    }

    public void TryPlaceTower(Vector3 position, TowerSpot spot)
    {
        if (selectedTower == null || spot.IsOccupied || !CanAffordSelectedTower())
        {
            Debug.LogWarning("Cannot place tower!");
            return;
        }

        GameObject tower = Instantiate(selectedTower.towerPrefab, position, Quaternion.identity);
        spot.SetOccupied(true, tower); // Передаем ссылку на башню

        SpendMoney(selectedTower.cost);

        Debug.Log($"Created tower: {tower?.name}");
        Debug.Log($"Passing tower to spot: {spot.gameObject.name}");

        selectedTower = null;
    }

    public void RemoveTower(TowerSpot spot)
    {
        if (!spot.IsOccupied)
        {
            Debug.LogWarning("No tower to remove!");
            return;
        }

        GameObject tower = spot.GetPlacedTower();
        if (tower != null)
        {
            Debug.Log($"Destroying tower: {tower.name}");
            RefundMoney(spot.GetRefundAmount());
            spot.SetOccupied(false, tower);
            Destroy(tower);

            Debug.Log($"Removing tower: {tower?.name} from spot: {spot.gameObject.name}");
        }
        else
        {
            Debug.LogWarning("Tower reference is null!");
        }
    }

    public void RefundMoney(int amount)
    {
        playerMoney += amount;
        Debug.Log($"Player money refunded: {amount}");
        UpdateGoldUI();
    }

}


