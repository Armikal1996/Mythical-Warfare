using UnityEngine;

public abstract class EnemyUnitBase : MonoBehaviour
{
    public UnitTeam team { get; private set; }
    protected string enemyUnitTag; // Determined based on team

    protected int currentHealth;
    protected float moveSpeed;
    protected int baseDamage;
    protected float attackSpeed;
    protected float attackRange = 1f;

    protected Transform playerBase;
    protected Transform target; // <--- unified target reference
    protected Transform currentTarget; 
    protected bool isAttackingUnit = false;


    public virtual void Initialize(UnitData data, UnitTeam unitTeam)
    {
        currentHealth = data.health;
        moveSpeed = data.moveSpeed;
        baseDamage = data.baseDamage;
        attackSpeed = data.attackSpeed;
        attackRange = data.attackRange;
        team = unitTeam;

        switch (team)
        {
            case UnitTeam.Player:
                target = GameObject.FindGameObjectWithTag("EnemyBase")?.transform;
                currentTarget = target;
                enemyUnitTag = "EnemyUnit";
                gameObject.tag = "PlayerUnit";

                var collider = GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    Vector2 offset = collider.offset;
                    offset.x *= -1;
                    collider.offset = offset;
                }
                break;
            case UnitTeam.Enemy:
                target = GameObject.FindGameObjectWithTag("PlayerBase")?.transform;
                currentTarget = target;
                enemyUnitTag = "PlayerUnit";
                gameObject.tag = "EnemyUnit";
                break;
        }
    }

    protected virtual void Update()
    {
        if (currentTarget == null)
        {
            currentTarget = target; // fallback to base
            isAttackingUnit = false;
        }

        // If not in range, keep moving toward current target
        if (!IsInAttackRange())
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
        }
    }

    protected virtual bool IsInAttackRange()
    {
        if (currentTarget == null) return false;

        float distance = Vector2.Distance(transform.position, currentTarget.position);
        return distance <= attackRange;
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void StartAttackingTarget(Collider2D enemyUnit) { }
    protected virtual void StartAttackingBase(Collider2D baseCol) { }

}
