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

//        // Устанавливаем данные кнопки
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

//        // Настраиваем иконку
//        iconImage.sprite = option.icon;

//        // Привязываем действие к кнопке
//        button.onClick.AddListener(() =>
//        {
//            if (TowerManager.Instance != null)
//            {
//                TowerManager.Instance.SelectTower(towerOption);
//            }
//        });
//    }
//}


