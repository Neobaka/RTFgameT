using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    private Tower currentTower;
    private bool isOccupied = false;
    public bool IsOccupied { get; private set; }
    private Material defaultMaterial;
    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            defaultMaterial = rend.material;
    }

    public void SetOccupied(bool occupied)
    {
        IsOccupied = occupied;
    }

    public void ResetMaterial()
    {
        if (rend != null)
            rend.material = defaultMaterial;
    }
    private void OnMouseDown()
    {
        Debug.Log("TowerSpot clicked!");

        if (!isOccupied && TowerManager.Instance != null)
        {
            Debug.Log("Spot is not occupied");
            if (TowerManager.Instance.selectedTower != null)
            {
                Vector3 position = transform.position + Vector3.up * 0.01f;
                GameObject towerObj = Instantiate(TowerManager.Instance.selectedTower.towerPrefab, position, Quaternion.identity);
                currentTower = towerObj.GetComponent<Tower>();
                SetOccupied(true);
                Debug.Log("Tower placed successfully");
            }
            else
            {
                Debug.Log("No tower selected in TowerManager");
            }
        }
        else
        {
            Debug.Log($"Spot is occupied or TowerManager is null. Occupied: {isOccupied}, Manager: {TowerManager.Instance != null}");
        }
    }

    //private void PlaceTower()
    //{
    //    Tower towerToPlace = BuildManager.Instance.GetSelectedTower();
    //    if (towerToPlace != null)
    //    {
    //        Debug.Log($"Placing tower: {towerToPlace.name}"); // Проверяем процесс размещения

    //        // Устанавливаем позицию немного выше спота, чтобы башня не проваливалась
    //        Vector3 position = transform.position + Vector3.up * 0.01f;

    //        currentTower = Instantiate(towerToPlace, position, Quaternion.identity);
    //        isOccupied = true;
    //        BuildManager.Instance.ClearSelectedTower();

    //        Debug.Log("Tower placed successfully"); // Подтверждаем размещение
    //    }
    //    else
    //    {
    //        Debug.Log("TowerToPlace is null!");
    //    }
    //}
}