using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float damage;
    [SerializeField] private float speed = 70f;
    [SerializeField] private float explosionRadius = 0f;
    [SerializeField] private GameObject impactEffect;

    public void Initialize(Transform _target, float _damage)
    {
        target = _target;
        damage = _damage;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        float distanceThisFrame = speed * Time.deltaTime;

        // Поворот снаряда в направлении цели
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        if (impactEffect != null)
        {
            GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effect, 2f);
        }

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            DamageEnemy(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                DamageEnemy(collider.transform);
            }
        }
    }

    void DamageEnemy(Transform enemyTransform)
    {
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (explosionRadius > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}