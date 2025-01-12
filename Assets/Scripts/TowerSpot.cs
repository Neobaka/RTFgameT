using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    private bool isOccupied;

    public bool IsOccupied => isOccupied;

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
    }

    public void OnMouseDown()
    {
        if (isOccupied)
        {
            Debug.LogWarning("This spot is already occupied!");
            return;
        }

        if (TowerManager.Instance != null && TowerManager.Instance.HasSelectedTower())
        {
            TowerManager.Instance.TryPlaceTower(transform.position, this);
        }
        else
        {
            Debug.LogWarning("No tower selected or invalid spot!");
        }
    }
}
