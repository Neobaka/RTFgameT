//using UnityEngine;

//public class TowerSpot : MonoBehaviour
//{
//    [SerializeField] private bool isOccupied = false;
//    private SpriteRenderer spotRenderer;

//    public bool IsOccupied => isOccupied;

//    private void Awake()
//    {
//        spotRenderer = GetComponent<SpriteRenderer>();
//    }

//    public void SetOccupied(bool state)
//    {
//        isOccupied = state;
//    }

//    private void OnMouseEnter()
//    {
//        Debug.Log("Mouse entered TowerSpot");
//        if (!isOccupied && TowerManager.Instance.HasSelectedTower())
//        {
//            // ������������ ����� ��� ����������
//            HighlightSpot(true);
//        }
//        // ���������� ������ ��������� �����
//        TowerManager.Instance.ShowPreview(transform.position);
//    }

//    private void OnMouseExit()
//    {
//        Debug.Log("Mouse exit TowerSpot");
//        // ������� ���������
//        HighlightSpot(false);
//        TowerManager.Instance.HidePreview();
//    }

//    private void OnMouseDown()
//    {
//        Debug.Log("Mouse clicked!");
//        if (!isOccupied && TowerManager.Instance.HasSelectedTower())
//        {
//            // ������ ���������� ����� �������������� � TowerManager
//            TowerManager.Instance.TryPlaceTower();
//        }
//    }

//    private void HighlightSpot(bool highlight)
//    {
//        if (spotRenderer != null)
//        {
//            Debug.Log("Color changed");
//            // ����� ��������� ���� ��������� �����
//            //spotRenderer.color = highlight ? Color.green, Color.a = 90 : Color.white, Color.a = 90;
//            if (highlight)
//            {
//                Color color = Color.green;
//                color.a = 0.3f;
//                spotRenderer.color = Color.green;
//            }
//            else
//            {
//                Color color = Color.white;
//                color.a = 0.3f;
//                spotRenderer.color = color;
//            }
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
        if (!isOccupied && TowerManager.Instance.HasSelectedTower())
        {
            HighlightSpot(true);
        }
    }

    private void OnMouseExit()
    {
        HighlightSpot(false);
    }

    private void OnMouseDown()
    {
        if (!isOccupied && TowerManager.Instance.HasSelectedTower())
        {
            TowerManager.Instance.TryPlaceTower(transform.position, this);
        }
    }

    private void HighlightSpot(bool highlight)
    {
        if (spotRenderer != null)
        {
            // ������ ���� ��������� � ����������� �� �����
            Color color = highlight ? Color.green : Color.white;
            color.a = 0.3f; // ����������������
            spotRenderer.color = color;
        }
    }
}


