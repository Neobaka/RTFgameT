using UnityEngine;

public class Tower : MonoBehaviour
{
    [System.Serializable]
    public class UpgradeStats
    {
        public float damageIncrease;
        public float rangeIncrease;
        public float fireRateIncrease;
        public int cost;
    }

    public enum TowerType
    {
        Basic,
        Sniper,
        Splash,
        Slow
    }

    public TowerType type;
    public int cost = 100;
    public Sprite icon;
    public UpgradeStats[] upgrades;

    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private Transform firePoint; // Точка, откуда вылетают снаряды


    public float Damage => damage;
    public float Range => range;
    public float FireRate => fireRate;

    private int currentLevel = 1;
    private float fireCountdown = 0f;
    private Transform target;

    public void Upgrade()
    {
        if (currentLevel >= upgrades.Length + 1)
            return;

        UpgradeStats upgrade = upgrades[currentLevel - 1];
        if (GameManager.Instance.SpendGold(upgrade.cost))
        {
            damage += upgrade.damageIncrease;
            range += upgrade.rangeIncrease;
            fireRate += upgrade.fireRateIncrease;
            currentLevel++;
            UpdateRangeIndicator();
        }
    }

    private void UpdateRangeIndicator()
    {
        if (rangeIndicator != null)
        {
            rangeIndicator.transform.localScale = Vector3.one * (range * 2);
        }
    }
    private void Start()
    {
        switch (type)
        {
            case TowerType.Sniper:
                damage *= 2f;
                range *= 1.5f;
                fireRate *= 0.5f;
                break;
            case TowerType.Splash:
                // Установите explosionRadius в префабе снаряда
                damage *= 0.7f;
                range *= 0.8f;
                break;
            case TowerType.Slow:
                damage *= 0.3f;
                range *= 1.2f;
                fireRate *= 1.5f;
                break;
        }

        UpdateRangeIndicator();
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        if (!IsTargetInRange())
        {
            target = null;
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }

    private bool IsTargetInRange()
    {
        return Vector3.Distance(transform.position, target.position) <= range;
    }

    private void Shoot()
    {
        // Поворот башни к цели
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        GameObject projectileGO = Instantiate(projectilePrefab,
        firePoint != null ? firePoint.position : transform.position,
        Quaternion.identity);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Initialize(target, damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}