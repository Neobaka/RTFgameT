using UnityEngine;
using System.Collections;

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
    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private int goldReward = 50;

    private float speed;
    private float hp;
    private float damage;
    private Transform[] waypoints;
    private int waypointIndex = 0;

    private Transform targetMainTower; // ���� - MainTower
    private bool isAttackingMainTower = false; // ��������, ����� �� ��������� �������� ��������� ���

    public void Initialize(Transform[] points)
    {
        waypoints = points;
        transform.position = waypoints[0].position;

        // ��������� ���������� � ����������� �� ���� �����
        switch (type)
        {
            case EnemyType.Default:
                speed = baseSpeed * 0.5f;
                hp = baseHP * 0.5f;
                damage = baseDamage * 0.5f;
                break;
            case EnemyType.PowerDefault:
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

    private void Start()
    {
        // ������� MainTower
        targetMainTower = GameObject.FindGameObjectWithTag("MainTower")?.transform;
    }

    private void Update()
    {
        Move();

        // ���� ���� ��������� MainTower, �������� �����
        if (targetMainTower != null && Vector3.Distance(transform.position, targetMainTower.position) < 9f)
        {
            AttackMainTower();
        }
    }

    private void Move()
    {
        // ��������� �����, ���� �� ������ � MainTower
        if (targetMainTower != null && Vector3.Distance(transform.position, targetMainTower.position) < 8f)
        {
            return;
        }

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

        // �������� � ��������� �����
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // ���� �������� �����, ������� � ���������
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            waypointIndex++;
        }
    }

    private void AttackMainTower()
    {
        if (!isAttackingMainTower)
        {
            StartCoroutine(AttackMainTowerRoutine());
        }
    }

    private IEnumerator AttackMainTowerRoutine()
    {
        isAttackingMainTower = true;
        Debug.Log("Started attacking MainTower!");

        while (targetMainTower != null && Vector3.Distance(transform.position, targetMainTower.position) < 9f)
        {
            Debug.Log("Enemy is attacking MainTower...");
            Tower mainTower = targetMainTower.GetComponent<Tower>();
            if (mainTower != null)
            {
                Debug.Log("����� ����������!");
                mainTower.TakeDamage(damage);
                Debug.Log($"MainTower took {damage} damage.");
            }
            else Debug.Log("����� ����� null!");

            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Stopped attacking MainTower.");
        isAttackingMainTower = false;
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
