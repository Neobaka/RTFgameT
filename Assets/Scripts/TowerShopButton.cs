//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;


//public class TowerButton : MonoBehaviour
//{
//    [SerializeField] private Image iconImage;
//    [SerializeField] private TextMeshProUGUI costText;

//    private TowerManager.TowerOption towerOption;

//    public void Initialize(TowerManager.TowerOption option)
//    {
//        towerOption = option;

//        // ������������� ������ ������
//        iconImage.sprite = option.icon;
//        costText.text = option.cost.ToString();
//    }

//    public void OnClick()
//    {
//        if (TowerManager.Instance != null)
//        {
//            TowerManager.Instance.SelectTower(towerOption);
//        }
//    }
//}
//using UnityEngine;
//using UnityEngine.UI;

//public class TowerShopButton : MonoBehaviour
//{
//    [SerializeField] private Image iconImage;
//    [SerializeField] private Button button;

//    private TowerManager.TowerOption towerOption;

//    public void Initialize(TowerManager.TowerOption option)
//    {
//        towerOption = option;

//        // ����������� ������
//        iconImage.sprite = option.icon;

//        // ����������� �������� � ������
//        button.onClick.AddListener(() =>
//        {
//            if (TowerManager.Instance != null)
//            {
//                TowerManager.Instance.SelectTower(towerOption);
//            }
//        });
//    }
//}


