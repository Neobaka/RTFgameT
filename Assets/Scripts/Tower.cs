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
    [SerializeField] private Transform firePoint;
    [SerializeField] private RectTransform hpBar;

    public float Damage => damage;
    public float Range => range;
    public float FireRate => fireRate;

    private int currentLevel = 1;
    private float fireCountdown = 0f;
    private Transform target;

    public float maxHealth = 100f;
    private float health = 100f;
    public bool isMainTower = false;

    private GameObject initialProjectilePrefab;

    private void Start()
    {
        initialProjectilePrefab = projectilePrefab;
        // ������������� ������� �����
        if (type == TowerType.MainTower)
        {
            isMainTower = true;
            health = maxHealth;
        }
        UpdateRangeIndicator();
    }

    private void Update()
    {
        if (projectilePrefab == null && initialProjectilePrefab != null)
        {
            Debug.Log($"{gameObject.name}: �������������� �������� projectilePrefab.");
            projectilePrefab = initialProjectilePrefab;
        }

        if (isMainTower && health <= 0f)
        {
            Destroy(gameObject);
            GameManager.Instance.GameOver();
            return;
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
    //Debug.Log($"������� ������: {enemies.Length}");

    float shortestDistance = Mathf.Infinity;
    GameObject nearestEnemy = null;

    foreach (GameObject enemy in enemies)
    {
        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        //Debug.Log($"���������� �� {enemy.name}: {distanceToEnemy}");

        if (distanceToEnemy < shortestDistance)
        {
            shortestDistance = distanceToEnemy;
            nearestEnemy = enemy;
        }
    }

    if (nearestEnemy != null && shortestDistance <= range)
    {
        target = nearestEnemy.transform;
        Debug.Log($"���� �����������: {target.name}");
    }
    else
    {
        target = null;
        //Debug.Log("���� �� �������.");
    }
}


    private bool IsTargetInRange()
    {
        //if (target != null && Vector3.Distance(transform.position, target.position) <= range)
        //{
        //    Debug.Log($"{gameObject.name}: ���� � ���� ���������.");
        //    return true;
        //}
        //else
        //{
        //    Debug.Log($"{gameObject.name}: ���� ��� ������������ ��� �� ������.");
        //    return false ;
        //}
        //return false;
        return target != null && Vector3.Distance(transform.position, target.position) <= range;
    }

    private void Shoot()
    {
        Debug.Log($"{gameObject.name}: ����� Shoot() ������.");

        if (projectilePrefab == null || target == null)
        {
            Debug.LogError($"{gameObject.name}: Prefab ������� �� �����!");
            return;
        }

        GameObject projectileGO = Instantiate(projectilePrefab,
            firePoint != null ? firePoint.position : transform.position,
            Quaternion.identity);
        Debug.Log($"������ {projectileGO.name} ������ � ������� {firePoint.position}");


        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            Debug.Log("����������� ������");
            projectile.Initialize(target, damage);
            Debug.Log($"{gameObject.name} �������� � {target.name}");
        }
        else Debug.LogError("�� ������� ����������� ��������� Projectile!");
    }

    public void TakeDamage(float damageAmount)
    {
        if (isMainTower)
        {
            health -= damageAmount;
            UpdateHpBar();
        }
    }

    private void UpdateRangeIndicator()
    {
        if (rangeIndicator != null)
        {
            rangeIndicator.transform.localScale = Vector3.one * (range * 2);
        }
    }

    public void UpdateHpBar()
    {
        if (hpBar != null)
        {
            float healthPercentage = health / maxHealth;
            hpBar.localScale = new Vector3(healthPercentage, 1f, 1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
