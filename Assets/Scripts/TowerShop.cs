////using UnityEngine;
////using UnityEngine.UI;

////public class TowerShop : MonoBehaviour
////{
////    public static TowerShop Instance { get; private set; }

////    [System.Serializable]
////    public class TowerButton
////    {
////        public GameObject towerPrefab;
////        public int cost;
////        public Button button;
////    }

////    public TowerButton[] towerButtons;
////    private GameObject selectedTowerPrefab;
////    public float previewAlpha = 0.5f; // Прозрачность предварительного просмотра
////    private GameObject previewTower; // Объект предварительного просмотра

////    void Awake()
////    {
////        if (Instance == null)
////            Instance = this;
////        else
////            Destroy(gameObject);
////    }

////    void Start()
////    {
////        // Настраиваем кнопки
////        for (int i = 0; i < towerButtons.Length; i++)
////        {
////            int index = i; // Сохраняем индекс для использования в лямбда-выражении
////            towerButtons[i].button.onClick.AddListener(() => SelectTower(index));
////        }
////    }

////    public void SelectTower(int index)
////    {
////        if (index < 0 || index >= towerButtons.Length) return;//Debug.LogError("Индекс кнопки не в массиве кнопок");

////        selectedTowerPrefab = towerButtons[index].towerPrefab;

////        // Уничтожаем предыдущий превью если он существует
////        if (previewTower != null)
////            Destroy(previewTower);

////        // Создаем новый превью
////        CreatePreviewTower();
////    }

////    private void CreatePreviewTower()
////    {
////        if (selectedTowerPrefab == null) return;

////        previewTower = Instantiate(selectedTowerPrefab);

////        // Делаем башню полупрозрачной для предпросмотра
////        SpriteRenderer[] renderers = previewTower.GetComponentsInChildren<SpriteRenderer>();
////        foreach (SpriteRenderer renderer in renderers)
////        {
////            Material material = renderer.material;
////            Color color = material.color;
////            color.a = previewAlpha;
////            material.color = color;
////        }

////        previewTower.SetActive(false);
////    }

////    public GameObject GetSelectedTower()
////    {
////        return selectedTowerPrefab;
////    }

////    public void ShowPreview(Vector3 position)
////    {
////        if (previewTower != null)
////        {
////            previewTower.SetActive(true);
////            previewTower.transform.position = position;
////        }
////    }

////    public void HidePreview()
////    {
////        if (previewTower != null)
////        {
////            previewTower.SetActive(false);
////        }
////    }
////}
//using UnityEngine;
//using UnityEngine.UI;

//public class TowerShop : MonoBehaviour
//{
//    public static TowerShop Instance { get; private set; }

//    [System.Serializable]
//    public class TowerButton
//    {
//        public GameObject towerPrefab;
//        public int cost;
//        public Button button;
//    }

//    public TowerButton[] towerButtons;

//    void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    void Start()
//    {
//        InitializeButtons();
//    }

//    private void InitializeButtons()
//    {
//        for (int i = 0; i < towerButtons.Length; i++)
//        {
//            int index = i; // Для использования в лямбда-выражении
//            towerButtons[i].button.onClick.AddListener(() => SelectTower(index));
//        }
//    }

//    public void SelectTower(int index)
//    {
//        if (index < 0 || index >= towerOptions.Length)
//        {
//            Debug.LogError("Invalid tower selection index!");
//            return;
//        }

//        TowerManager.Instance.SelectTower(towerOptions[index]);
//        Debug.Log($"Selected tower: {towerOptions[index].towerName}");
//    }
//}
using UnityEngine;

public class TowerShop : MonoBehaviour
{
    public void SelectTower(int index)
    {
        if (TowerManager.Instance != null)
        {
            TowerManager.Instance.SelectTower(index);
        }
    }
}
//using System.Collections.Generic;
//using UnityEngine;

//public class TowerShop : MonoBehaviour
//{
//    [SerializeField] private GameObject towerButtonPrefab;
//    [SerializeField] private Transform buttonContainer;

//    private Dictionary<TowerManager.TowerOption, GameObject> towerButtons = new();

//    public void InitializeShop(TowerManager.TowerOption[] towerOptions)
//    {
//        foreach (var option in towerOptions)
//        {
//            // Создаем кнопку
//            GameObject buttonObj = Instantiate(towerButtonPrefab, buttonContainer);
//            TowerShopButton button = buttonObj.GetComponent<TowerShopButton>();
//            button.Initialize(option);

//            // Сохраняем связь между кнопкой и опцией
//            towerButtons.Add(option, buttonObj);
//        }
//    }

//    public void RemoveTowerButton(TowerManager.TowerOption towerOption)
//    {
//        if (towerButtons.TryGetValue(towerOption, out GameObject buttonObj))
//        {
//            Destroy(buttonObj); // Удаляем кнопку из магазина
//            towerButtons.Remove(towerOption); // Убираем из словаря
//        }
//    }
//}



