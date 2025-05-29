using System.Collections;
using UnityEngine;

public class TankEnemyUnit : EnemyUnitBase
{
    private Coroutine attackCoroutine;
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
            attackCoroutine = StartCoroutine(DamageUnitCoroutine(unit));
        }
    }

    protected override void StartAttackingBase(Collider2D baseCol)
    {
        var baseRef = baseCol.GetComponent<BaseHealth>();
        if (baseRef != null)
        {
            attackCoroutine = StartCoroutine(DamageBaseCoroutine(baseRef));
        }
    }

    private IEnumerator DamageUnitCoroutine(EnemyUnitBase unit)
    {
        while (unit != null)
        {
            unit.TakeDamage(baseDamage);
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private IEnumerator DamageBaseCoroutine(BaseHealth baseRef)
    {
        while (baseRef != null)
        {
            baseRef.TakeDamage(baseDamage);
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    protected override bool IsInAttackRange() => attackCoroutine != null;
}
