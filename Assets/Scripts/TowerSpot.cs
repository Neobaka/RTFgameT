//using UnityEngine;

//public class TowerSpot : MonoBehaviour
//{
//    private bool isOccupied;

//    public bool IsOccupied => isOccupied;

//    public void SetOccupied(bool occupied)
//    {
//        isOccupied = occupied;
//    }

//    public void OnMouseDown()
//    {
//        if (isOccupied)
//        {
//            Debug.LogWarning("This spot is already occupied!");
//            return;
//        }

//        if (TowerManager.Instance != null && TowerManager.Instance.HasSelectedTower())//HasSelectedTower
//        {
//            TowerManager.Instance.TryPlaceTower(transform.position, this);
//        }
//        else
//        {
//            Debug.LogWarning("No tower selected or invalid spot!");
//        }
//    }
//}
using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    [SerializeField] private bool isOccupied = false;
    private SpriteRenderer spotRenderer;

    public bool IsOccupied => isOccupied;

    private void Awake()
    {
        spotRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetOccupied(bool state)
    {
        isOccupied = state;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse entered TowerSpot");
        if (!isOccupied && TowerManager.Instance.HasSelectedTower())
        {
            // Подсвечиваем место для размещения
            HighlightSpot(true);
        }
        // Показываем превью выбранной башни
        TowerManager.Instance.ShowPreview(transform.position);
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse exit TowerSpot");
        // Убираем подсветку
        HighlightSpot(false);
        TowerManager.Instance.HidePreview();
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse clicked!");
        if (!isOccupied && TowerManager.Instance.HasSelectedTower())
        {
            // Логика размещения башни обрабатывается в TowerManager
            TowerManager.Instance.TryPlaceTower();
        }
    }

    private void HighlightSpot(bool highlight)
    {
        if (spotRenderer != null)
        {
            Debug.Log("Color changed");
            // Можно настроить цвет подсветки здесь
            //spotRenderer.color = highlight ? Color.green, Color.a = 90 : Color.white, Color.a = 90;
            if (highlight)
            {
                Color color = Color.green;
                color.a = 0.3f;
                spotRenderer.color = color;
            }
            else
            {
                Color color = Color.white;
                color.a = 0.3f;
                spotRenderer.color = color;
            }
        }
    }
}
