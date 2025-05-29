using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUnitSpawner : MonoBehaviour
{
    [Header("Unit Data")]
    public UnitData simpleUnitData;
    public UnitData rangedUnitData;
    public UnitData tankUnitData;

    [Header("Unit Prefabs")]
    public GameObject simpleUnitPrefab;
    public GameObject rangedUnitPrefab;
    public GameObject tankUnitPrefab;

    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public int simpleUnitCost = 5;
    public int rangedUnitCost = 10;
    public int tankUnitCost = 15;

    [Header("Meat")]
    public int meatAmount = 20;

    private Label meatCountLabel;
    private Button simpleButton;
    private Button rangedButton;
    private Button tankButton;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        meatCountLabel = root.Q<Label>("MeatCountLabel");
        simpleButton = root.Q<Button>("SpawnSimpleUnit");
        rangedButton = root.Q<Button>("SpawnRangedUnit");
        tankButton = root.Q<Button>("SpawnTankUnit");

        simpleButton.clicked += () => TrySpawnUnit(simpleUnitPrefab, simpleUnitData, simpleUnitCost);
        rangedButton.clicked += () => TrySpawnUnit(rangedUnitPrefab, rangedUnitData, rangedUnitCost);
        tankButton.clicked += () => TrySpawnUnit(tankUnitPrefab, tankUnitData, tankUnitCost);


        UpdateUI();
    }

    private void TrySpawnUnit(GameObject prefab, UnitData data, int cost)
    {
        if (meatAmount >= cost)
        {
            meatAmount -= cost;
            GameObject unit = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

            // Set team and tag
            var baseUnit = unit.GetComponent<EnemyUnitBase>();
            if (baseUnit != null)
            {
                baseUnit.Initialize(data, UnitTeam.Player); // Pass proper UnitData if needed
            }

            unit.tag = "PlayerUnit";
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough meat!");
        }
    }

    private void UpdateUI()
    {
        if (meatCountLabel != null)
            meatCountLabel.text = $"Meat: {meatAmount}";
    }
}
