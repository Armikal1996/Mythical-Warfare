using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WaveSet
{
    DemoWaveSet1,
    DemoWaveSet2
    // Add more if needed
}

public class EnemyBase : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public WaveSet selectedWaveSet;

    public List<EnemyWave> demoWaveSet1;
    public List<EnemyWave> demoWaveSet2;

    private Transform spawnPoint;

    private void Start()
    {
        currentHealth = maxHealth;
        spawnPoint = this.transform;
        
        List<EnemyWave> chosenSet = GetSelectedWaveSet();
        StartCoroutine(SpawnWaves(chosenSet));
    }

    private IEnumerator SpawnWaves(List<EnemyWave> waves)
    {
        foreach (var wave in waves)
        {
            yield return new WaitForSeconds(wave.waveDelay);

            foreach (var unitData in wave.units)
            {
                Vector3 randomOffset = Random.insideUnitCircle * 0.2f;

                GameObject unit = Instantiate(unitData.unitPrefab, spawnPoint.position + new Vector3(randomOffset.x, 0, randomOffset.y), Quaternion.identity);
                EnemyUnitBase enemy = unit.GetComponent<EnemyUnitBase>();

                if (enemy != null)
                {
                    enemy.Initialize(unitData, UnitTeam.Enemy);
                }
            }
        }
    }
    private List<EnemyWave> GetSelectedWaveSet()
    {
        return selectedWaveSet switch
        {
            WaveSet.DemoWaveSet1 => demoWaveSet1,
            WaveSet.DemoWaveSet2 => demoWaveSet2,
            _ => demoWaveSet1
        };
    }
}
