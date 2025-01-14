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
            if (target.CompareTag("Enemy")) // Проверяем, что target — это враг
            {
                Destroy(target.gameObject); // Уничтожаем объект врага
                Debug.Log($"{target.name} был уничтожен!");
            }
            else
            {
                Debug.LogWarning("Попытка уничтожить объект, который не является врагом!");
            }

            target = null; // Сбрасываем ссылку на цель
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
    //    // Подписываемся на событие смерти врага
    //    Enemy enemy = GetComponent<Enemy>();
    //    if (enemy != null)
    //    {
    //        enemy.OnDeath += HandleEnemyDeath; // Обрабатываем событие смерти
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

        Destroy(gameObject); // Уничтожаем снаряд после попадания

        // Убираем снаряд из списка активных снарядов врага
        if (enemyTarget != null)
        {
            enemyTarget.RemoveProjectile(gameObject);
        }
    }

    private void Explode()
    {
        Debug.Log($"Взрыв происходит в позиции: {transform.position}");
        // Находим все коллайдеры в радиусе взрыва
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius);

        Debug.Log($"Обработано {hitEnemies.Length} врагов в радиусе взрыва");

        foreach (Collider enemyCollider in hitEnemies)
        {
            // Проверяем, что объект имеет тег "Enemy"
            if (enemyCollider.CompareTag("Enemy"))
            {
                // Получаем компонент Enemy
                Enemy enemyInstance = enemyCollider.GetComponent<Enemy>();
                Debug.Log($"Враг в позиции: {enemyInstance.transform.position}");
                if (enemyInstance != null)
                {
                    Debug.Log($"Враг {enemyInstance.name} получил {damage} урона.");
                    enemyInstance.TakeDamage(damage);
                }
                else
                {
                    Debug.LogWarning("Компонент Enemy не найден на объекте.");
                }
            }
            else
            {
                Debug.Log("Не враг в радиусе взрыва: " + enemyCollider.name);
            }
        }

        Destroy(gameObject); // Уничтожаем снаряд после взрыва
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
    //    // Логика при смерти врага
    //    Debug.Log("Враг уничтожен!");

    //    // Уничтожаем снаряд, если он существует
    //    if (projectile != null)
    //    {
    //        Debug.Log("Уничтожение снаряда...");
    //        Destroy(projectile); // Уничтожаем снаряд
    //    }

    //    // Уничтожаем врага, если это еще не было сделано
    //    if (enemy != null)
    //    {
    //        Destroy(enemy); // Уничтожаем врага
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
