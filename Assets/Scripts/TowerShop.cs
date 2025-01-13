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
//    public float previewAlpha = 0.5f; // ������������ ���������������� ���������
//    private GameObject previewTower; // ������ ���������������� ���������

//    void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(gameObject);
//    }

//    void Start()
//    {
//        // ����������� ������
//        for (int i = 0; i < towerButtons.Length; i++)
//        {
//            int index = i; // ��������� ������ ��� ������������� � ������-���������
//            towerButtons[i].button.onClick.AddListener(() => SelectTower(index));
//        }
//    }

//    public void SelectTower(int index)
//    {
//        if (index < 0 || index >= towerButtons.Length) return;

//        selectedTowerPrefab = towerButtons[index].towerPrefab;

//        // ���������� ���������� ������ ���� �� ����������
//        if (previewTower != null)
//            Destroy(previewTower);

//        // ������� ����� ������
//        CreatePreviewTower();
//    }

//    private void CreatePreviewTower()
//    {
//        if (selectedTowerPrefab == null) return;

//        previewTower = Instantiate(selectedTowerPrefab);

//        // ������ ����� �������������� ��� �������������
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
    public float previewAlpha = 0.5f; // ������������ ���������������� ���������
    private GameObject previewTower; // ������ ���������������� ���������

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // ����������� ������
        for (int i = 0; i < towerButtons.Length; i++)
        {
            int index = i; // ��������� ������ ��� ������������� � ������-���������
            towerButtons[i].button.onClick.AddListener(() => SelectTower(index));
        }
    }

    public void SelectTower(int index)
    {
        if (index < 0 || index >= towerButtons.Length) return;

        selectedTowerPrefab = towerButtons[index].towerPrefab;

        // ���������� ���������� ������ ���� �� ����������
        if (previewTower != null)
            Destroy(previewTower);

        // ������� ����� ������
        CreatePreviewTower();
    }

    private void CreatePreviewTower()
    {
        if (selectedTowerPrefab == null) return;

        previewTower = Instantiate(selectedTowerPrefab);

        // ������ ����� �������������� ��� �������������
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