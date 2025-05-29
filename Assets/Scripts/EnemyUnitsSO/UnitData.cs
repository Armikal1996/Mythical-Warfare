using UnityEngine;

[CreateAssetMenu(fileName = "New UnitData", menuName = "Units/UnitData")]
public class UnitData : ScriptableObject
{
    public GameObject unitPrefab;
    public float moveSpeed;
    public int baseDamage;
    public float attackSpeed;
    public int health;
    public float attackRange;
    public bool isRanged;
    public bool canCharge;
    public bool canSpawnOtherUnits;

}
public enum UnitTeam
{
    Player,
    Enemy
}