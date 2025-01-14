using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float damage;
    [SerializeField] private float speed = 70f;
    [SerializeField] private float explosionRadius = 0f;
    [SerializeField] private GameObject impactEffect;

    private Enemy enemyTarget;

    public void Initialize(Transform _target, float _damage)
    {
        target = _target;
        damage = _damage;

        enemyTarget = _target.GetComponent<Enemy>();
        if (enemyTarget != null)
        {
            enemyTarget.AddProjectile(gameObject);
        }
    }

    private void Update()
    {
        if (target == null)
        {
            if (target.CompareTag("Enemy")) // ���������, ��� target � ��� ����
            {
                Destroy(target.gameObject); // ���������� ������ �����
                Debug.Log($"{target.name} ��� ���������!");
            }
            else
            {
                Debug.LogWarning("������� ���������� ������, ������� �� �������� ������!");
            }

            target = null; // ���������� ������ �� ����
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction * distanceThisFrame, Space.World);
    }

    //void Start()
    //{
    //    // ������������� �� ������� ������ �����
    //    Enemy enemy = GetComponent<Enemy>();
    //    if (enemy != null)
    //    {
    //        enemy.OnDeath += HandleEnemyDeath; // ������������ ������� ������
    //    }
    //}

    private void HitTarget()
    {
        if (impactEffect != null)
        {
            GameObject effectInstance = Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(effectInstance, 2f);
        }

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            DamageEnemy(target);
        }

        Destroy(gameObject); // ���������� ������ ����� ���������

        // ������� ������ �� ������ �������� �������� �����
        if (enemyTarget != null)
        {
            enemyTarget.RemoveProjectile(gameObject);
        }
    }

    private void Explode()
    {
        Debug.Log($"����� ���������� � �������: {transform.position}");
        // ������� ��� ���������� � ������� ������
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius);

        Debug.Log($"���������� {hitEnemies.Length} ������ � ������� ������");

        foreach (Collider enemyCollider in hitEnemies)
        {
            // ���������, ��� ������ ����� ��� "Enemy"
            if (enemyCollider.CompareTag("Enemy"))
            {
                // �������� ��������� Enemy
                Enemy enemyInstance = enemyCollider.GetComponent<Enemy>();
                Debug.Log($"���� � �������: {enemyInstance.transform.position}");
                if (enemyInstance != null)
                {
                    Debug.Log($"���� {enemyInstance.name} ������� {damage} �����.");
                    enemyInstance.TakeDamage(damage);
                }
                else
                {
                    Debug.LogWarning("��������� Enemy �� ������ �� �������.");
                }
            }
            else
            {
                Debug.Log("�� ���� � ������� ������: " + enemyCollider.name);
            }
        }

        Destroy(gameObject); // ���������� ������ ����� ������
    }



    private void DamageEnemy(Transform enemyTransform)
    {
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    //void HandleEnemyDeath(GameObject enemy, GameObject projectile)
    //{
    //    // ������ ��� ������ �����
    //    Debug.Log("���� ���������!");

    //    // ���������� ������, ���� �� ����������
    //    if (projectile != null)
    //    {
    //        Debug.Log("����������� �������...");
    //        Destroy(projectile); // ���������� ������
    //    }

    //    // ���������� �����, ���� ��� ��� �� ���� �������
    //    if (enemy != null)
    //    {
    //        Destroy(enemy); // ���������� �����
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        if (explosionRadius > 0f)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
