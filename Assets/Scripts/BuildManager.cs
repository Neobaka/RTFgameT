//using UnityEngine;

//public class BuildManager : MonoBehaviour
//{
//    public static BuildManager Instance { get; private set; }

//    [SerializeField] private Tower[] availableTowers; // ������ ��������� �����
//    [SerializeField] private Transform shopPanel; // ������ ��������
//    [SerializeField] private GameObject towerButtonPrefab; // ������ ������ �����


//    private Tower selectedTower;

//    public bool HasTowerSelected => selectedTower != null;

//    private void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(gameObject);
//    }

//    public void SelectTower(Tower tower)
//    {
//        selectedTower = tower;
//        Debug.Log($"Selected tower: ");
//    }

//    public Tower GetSelectedTower()
//    {
//        return selectedTower;
//    }

//    public void ClearSelectedTower()
//    {
//        selectedTower = null;
//    }
    
//}