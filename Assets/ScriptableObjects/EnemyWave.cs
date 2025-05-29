using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWave
{
    public float waveDelay = 3f;
    public List<UnitData> units;
}
