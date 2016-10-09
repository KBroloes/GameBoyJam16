using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave {
    [Header("Frequency of enemies")]
    public int spawnRate = 5;

    [Header("Wave duration")]
    public float duration = 10;

    [Header("Enemy variants to spawn")]
    public List<Enemy> EnemyUnits;

    [Header("Where to spawn them")]
    public int minLane = 0;
    public int maxLane = 5;
}
