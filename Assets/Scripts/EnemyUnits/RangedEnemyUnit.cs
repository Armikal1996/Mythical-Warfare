using System.Collections;
using UnityEngine;

public class RangedEnemyUnit : EnemyUnitBase
{
    public GameObject projectilePrefab;
    private Coroutine attackCoroutine;
    private string enemyTag = "EnemyUnit";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(team == UnitTeam.Player ? "EnemyUnit" : "PlayerUnit"))
        {
            currentTarget = other.transform;
            isAttackingUnit = true;
            StartAttackingTarget(other);
        }
        else if (other.CompareTag(team == UnitTeam.Player ? "EnemyBase" : "PlayerBase"))
        {
            currentTarget = other.transform;
            isAttackingUnit = false;
            StartAttackingBase(other);
        }
    }

    protected override void StartAttackingTarget(Collider2D enemyUnit)
    {
        var unit = enemyUnit.GetComponent<EnemyUnitBase>();
        if (unit != null)
        {
            attackCoroutine = StartCoroutine(ShootAtTarget(unit.transform));
        }
    }

    protected override void StartAttackingBase(Collider2D baseCol)
    {
        var baseRef = baseCol.GetComponent<BaseHealth>();
        if (baseRef != null)
        {
            attackCoroutine = StartCoroutine(ShootAtTarget(baseRef.transform));
        }
    }

    private IEnumerator ShootAtTarget(Transform targetTransform)
    {
        while (targetTransform != null)
        {
            if (projectilePrefab != null)
            {
                GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Projectile projectileScript = proj.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    projectileScript.Init(targetTransform.position, baseDamage);
                }
            }

            yield return new WaitForSeconds(attackSpeed);
        }
    }

    protected override bool IsInAttackRange() => attackCoroutine != null;
}
