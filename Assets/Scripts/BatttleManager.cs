using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void BattleLost()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("Game Over! You lost.");
        // TODO: Trigger UI, stop enemy spawns, etc.
    }
    public void BattleWon()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("Game Over! You WON.");
        // TODO: Trigger UI, stop enemy spawns, etc.
    }
}