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
