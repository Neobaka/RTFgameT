using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Normal,
        Default,
        PowerDefault,
        WithShield,
        Sniper,
        Boss
    }

    public EnemyType type;
    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] private float baseHP = 100f;
    [SerializeField] private float baseDamage = 100f;
    [SerializeField] private int goldReward = 50;

    private float speed;
    private float hp;
    private float damage;
    private Transform[] waypoints;
    public int waypointIndex = 0;

    public void Initialize(Transform[] points)
    {
        waypoints = points;
        transform.position = waypoints[0].position;
        Debug.Log($"Initialized with {points.Length} waypoints");

        // Настройка характеристик в зависимости от типа врага
        switch (type)
        {
            case EnemyType.Default:
                speed = baseSpeed * 0.5f;
                hp = baseHP * 0.5f;
                damage = baseDamage * 0.5f;
                break;
            case EnemyType.PowerDefault :
                speed = baseSpeed * 0.3f;
                hp = baseHP * 0.7f;
                damage = baseDamage * 0.7f;
                break;
            case EnemyType.WithShield:
                speed = baseSpeed * 0.7f;
                hp = baseHP * 0.3f;
                damage = baseDamage * 0.7f;
                break;
            case EnemyType.Sniper:
                speed = baseSpeed * 0.5f;
                hp = baseHP * 0.5f;
                damage = baseDamage * 0.5f;
                break;
            case EnemyType.Boss:
                speed = baseSpeed * 0.2f;
                hp = baseHP * 1f;
                damage = baseDamage * 1f;
                break;
        }
    }

    private void Update()
    {
        Debug.Log($"Waypoints in Move: {waypoints?.Length ?? 0}");
        Move();
    }

    private void Move()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoints are null or empty!");
            return;
        }

        if (waypointIndex >= waypoints.Length)
        {
            ReachEnd();
            return;
        }

        Vector3 targetPosition = waypoints[waypointIndex].position;

        // Движение к текущей точке
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // Проверка достижения текущей точки
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            waypointIndex++;
            Debug.Log($"Reached waypoint {waypointIndex}");
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.AddGold(goldReward);
        Destroy(gameObject);
    }

    private void ReachEnd()
    {
        GameManager.Instance.ReduceHealth();
        Destroy(gameObject);
    }
}