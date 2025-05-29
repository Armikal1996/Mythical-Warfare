using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int health = 100;
    public bool isEnemy;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(health + ": remaing health");
        if (health <= 0 && isEnemy)
        {
            Debug.Log($"{gameObject.name} destroyed!");
            BattleManager.Instance.BattleWon();
        }
        else if(health <=0 && !isEnemy)
        {
            Debug.Log($"{gameObject.name} destroyed!");
            BattleManager.Instance.BattleLost();
        }
    }
}
