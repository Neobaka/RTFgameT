using UnityEngine;
using UnityEngine.UI;

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
        MainTower,
        SniperTower,
        CatapultTower,
        ElectricTower,
        MagicTower,
        FireTower,
        IceTower,
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
    [SerializeField] private Transform firePoint; // �����, ������ �������� �������
    [SerializeField] private RectTransform hpBar; // ������ �� RectTransform HP ���� (UI ������)

    public float Damage => damage;
    public float Range => range;
    public float FireRate => fireRate;

    private int currentLevel = 1;
    private float fireCountdown = 0f;
    private Transform target;

    // ��� MainTower
    public float maxHealth = 100f;
    private float health = 100f; // �������� ������ ��� MainTower
    public bool isMainTower = false; // ��� MainTower

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
        // ������������� ������� ��� ������ �����
        switch (type)
        {
            case TowerType.MainTower:
                isMainTower = true;
                health = maxHealth; // ��������� �������� ��� MainTower
                break;
            case TowerType.SniperTower:
                damage *= 2f;
                range *= 1.5f;
                fireRate *= 0.5f;
                break;
            case TowerType.CatapultTower:
                damage *= 1.5f; // ��������� ���� ��� ����������
                range *= 1.2f;
                break;
            case TowerType.ElectricTower:
                damage *= 1.2f;
                range *= 1.3f;
                fireRate *= 0.8f;
                break;
            case TowerType.MagicTower:
                damage *= 1.1f;
                range *= 1.2f;
                fireRate *= 1.1f;
                break;
            case TowerType.FireTower:
                damage *= 1.5f;
                range *= 1.1f;
                fireRate *= 0.9f;
                break;
            case TowerType.IceTower:
                damage *= 1.2f;
                range *= 1.2f;
                fireRate *= 0.9f;
                break;
        }

        UpdateRangeIndicator();
    }

    private void Update()
    {
        if (isMainTower && health <= 0f)
        {
            Destroy(gameObject); // ���������� MainTower, ���� �������� <= 0
            GameManager.Instance.GameOver();
        }

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
        GameObject projectileGO = Instantiate(projectilePrefab,
            firePoint != null ? firePoint.position : transform.position,
            Quaternion.identity);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            // ������������� ������� � ����������� �� ���� �����
            switch (type)
            {
                case TowerType.MainTower:
                case TowerType.SniperTower:
                    projectile.Initialize(target, damage);
                    break;
                case TowerType.CatapultTower:
                    // ������ ��� ���������� (��������, ������� � �������� �������)
                    projectile.Initialize(target, damage);
                    break;
                case TowerType.ElectricTower:
                    // ������ ��� ������������� �������� � ��������
                    projectile.Initialize(target, damage);
                    break;
                case TowerType.MagicTower:
                    // ������ ��� ���������� ����
                    projectile.Initialize(target, damage);
                    break;
                case TowerType.FireTower:
                    // ������ ��� �������� ��������
                    projectile.Initialize(target, damage);
                    break;
                case TowerType.IceTower:
                    // ������ ��� ������� ��������
                    projectile.Initialize(target, damage);
                    break;
            }
        }
    }

    // ��� MainTower ��������� ����� ��������� �����
    public void TakeDamage(float damageAmount)
    {
        if (isMainTower)
        {
            health -= damageAmount;
            UpdateHpBar();
            GameManager.Instance.ReduceHealth();
        }
    }

    // ���������� ������ HP ����
    public void UpdateHpBar()
    {
        if (hpBar != null)
        {
            float healthPercentage = health / maxHealth;
            hpBar.localScale = new Vector3(healthPercentage, 1f, 1f); // ��������� ������
        }
    }

    // ��� ������ ����� ��������� ������ ��� ���������� ��������
    public void ApplyAreaEffect(Vector3 position, float radius, float damageAmount)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<Enemy>().TakeDamage(damageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
