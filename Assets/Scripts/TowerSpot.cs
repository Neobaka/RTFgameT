using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    [SerializeField] private bool isOccupied = false;
    private GameObject placedTower; // ������ �� ������������� �����
    private int refundAmount; // ����� �������� ��� �������� �����
    private SpriteRenderer spotRenderer;

    public bool IsOccupied => isOccupied;

    private void Awake()
    {
        spotRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetOccupied(bool state, GameObject tower = null)
    {
        isOccupied = state;
        placedTower = tower;

        Debug.Log($"Spot {gameObject.name} occupied: {state}, tower: {tower?.name}");

        if (tower != null)
        {
            // ���������� ������� ������� (50% ��������� �����)
            TowerManager.TowerOption option = TowerManager.Instance?.TowerOptions.Find(o => o.towerPrefab == tower);
            refundAmount = option != null ? option.cost / 2 : 0;
        }
        else
        {
            refundAmount = 0;
        }
    }

    public GameObject GetPlacedTower()
    {
        return placedTower;
        Debug.Log($"Returning placed tower: {placedTower?.name}");

    }

    public int GetRefundAmount()
    {
        return refundAmount;
    }

    private void OnMouseEnter()
    {
        if (!isOccupied && TowerManager.Instance.HasSelectedTower())
        {
            HighlightSpot(true);
        }
        Debug.Log("OnMouseEnter");
    }

    private void OnMouseExit()
    {
        HighlightSpot(false);
        Debug.Log("OnMouseExit");
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) // ����� ������ ����
        {
            Debug.Log("����� ������ ���� ������!");
            if (!isOccupied && TowerManager.Instance.HasSelectedTower())
            {
                // ��������� �����
                TowerManager.Instance.TryPlaceTower(transform.position, this);
            }
        }
        if (Input.GetMouseButtonDown(1)) // ������ ������ ����
        {
            Debug.Log($"Right mouse button clicked on spot: {gameObject.name}");
            if (isOccupied)
            {
                Debug.Log($"Spot is occupied, trying to remove tower: {GetPlacedTower().name}");
                TowerManager.Instance.RemoveTower(this);
            }
            else
            {
                Debug.Log("No tower to remove on this spot.");
            }
        }

    }

    private void HighlightSpot(bool highlight)
    {
        if (spotRenderer != null)
        {
            // ������ ���� ��������� � ����������� �� �����
            //Color color = highlight ? Color.green : Color.white;
            //color.a = 0.3f; // ����������������
            //spotRenderer.color = color;
            if (highlight)
            {
                Color color = Color.green;
                color.a = 0.3f;
                spotRenderer.color = color;
            }
            else
            {
                Color color = Color.white;
                color.a = 0.0f;
                spotRenderer.color = color;
            }
        }
    }
}
