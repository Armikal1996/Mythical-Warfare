using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    private int damage;
    private Vector3 target;

    public void Init(Vector3 targetPosition, int damageAmount)
    {
        target = targetPosition;
        damage = damageAmount;
        Destroy(gameObject, 5f); // fallback destroy
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBase") || other.CompareTag("EnemyBase"))
        {
            BaseHealth baseRef = other.GetComponent<BaseHealth>();
            if (baseRef != null)
            {
                baseRef.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
