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
//    private GameObject selectedTowerPrefab;
//    public float previewAlpha = 0.5f; // Прозрачность предварительного просмотра
//    private GameObject previewTower; // Объект предварительного просмотра

//    void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(gameObject);
//    }

//    void Start()
//    {
//        // Настраиваем кнопки
//        for (int i = 0; i < towerButtons.Length; i++)
//        {
//            int index = i; // Сохраняем индекс для использования в лямбда-выражении
//            towerButtons[i].button.onClick.AddListener(() => SelectTower(index));
//        }
//    }

//    public void SelectTower(int index)
//    {
//        if (index < 0 || index >= towerButtons.Length) return;

//        selectedTowerPrefab = towerButtons[index].towerPrefab;

//        // Уничтожаем предыдущий превью если он существует
//        if (previewTower != null)
//            Destroy(previewTower);

//        // Создаем новый превью
//        CreatePreviewTower();
//    }

//    private void CreatePreviewTower()
//    {
//        if (selectedTowerPrefab == null) return;

//        previewTower = Instantiate(selectedTowerPrefab);

//        // Делаем башню полупрозрачной для предпросмотра
//        Renderer[] renderers = previewTower.GetComponentsInChildren<Renderer>();
//        foreach (Renderer renderer in renderers)
//        {
//            Material material = renderer.material;
//            Color color = material.color;
//            color.a = previewAlpha;
//            material.color = color;
//        }

//        previewTower.SetActive(false);
//    }

//    public GameObject GetSelectedTower()
//    {
//        return selectedTowerPrefab;
//    }

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
//}
using UnityEngine;
using UnityEngine.UI;

public class TowerShop : MonoBehaviour
{
    public static TowerShop Instance { get; private set; }

    [System.Serializable]
    public class TowerButton
    {
        public GameObject towerPrefab;
        public int cost;
        public Button button;
    }

    public TowerButton[] towerButtons;
    private GameObject selectedTowerPrefab;
    public float previewAlpha = 0.5f; // Прозрачность предварительного просмотра
    private GameObject previewTower; // Объект предварительного просмотра

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Настраиваем кнопки
        for (int i = 0; i < towerButtons.Length; i++)
        {
            int index = i; // Сохраняем индекс для использования в лямбда-выражении
            towerButtons[i].button.onClick.AddListener(() => SelectTower(index));
        }
    }

    public void SelectTower(int index)
    {
        if (index < 0 || index >= towerButtons.Length) return;

        selectedTowerPrefab = towerButtons[index].towerPrefab;

        // Уничтожаем предыдущий превью если он существует
        if (previewTower != null)
            Destroy(previewTower);

        // Создаем новый превью
        CreatePreviewTower();
    }

    private void CreatePreviewTower()
    {
        if (selectedTowerPrefab == null) return;

        previewTower = Instantiate(selectedTowerPrefab);

        // Делаем башню полупрозрачной для предпросмотра
        SpriteRenderer[] renderers = previewTower.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            Material material = renderer.material;
            Color color = material.color;
            color.a = previewAlpha;
            material.color = color;
        }

        previewTower.SetActive(false);
    }

    public GameObject GetSelectedTower()
    {
        return selectedTowerPrefab;
    }

    public void ShowPreview(Vector3 position)
    {
        if (previewTower != null)
        {
            previewTower.SetActive(true);
            previewTower.transform.position = position;
        }
    }

    public void HidePreview()
    {
        if (previewTower != null)
        {
            previewTower.SetActive(false);
        }
    }
}