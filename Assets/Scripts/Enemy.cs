using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Normal,
        Fast,
        Tank,
        Flying
    }

    public EnemyType type;
    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] private float baseHealth = 100f;
    [SerializeField] private int goldReward = 50;

    private float speed;
    private float health;
    public Transform[] waypoints;
    public int waypointIndex = 0;

    public void Initialize(Transform[] points)
    {
        waypoints = points;
        transform.position = waypoints[0].position;
        Debug.Log($"Initialized with {points.Length} waypoints");

        // Настройка характеристик в зависимости от типа врага
        switch (type)
        {
            case EnemyType.Fast:
                speed = baseSpeed * 2f;
                health = baseHealth * 0.5f;
                break;
            case EnemyType.Tank:
                speed = baseSpeed * 0.5f;
                health = baseHealth * 2f;
                break;
            case EnemyType.Flying:
                speed = baseSpeed * 1.5f;
                health = baseHealth * 0.7f;
                break;
            default:
                speed = baseSpeed;
                health = baseHealth;
                break;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex >= waypoints.Length)
        {
            ReachEnd();
            return;
        }

        Vector3 targetPosition = waypoints[waypointIndex].position;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Добавим поворот в сторону движения
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

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
        health -= damage;
        if (health <= 0)
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