using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public GameObject unitPrefab;
    public UnitType unitType;

    [Header("Stats")]
    public int Life = 100;
    public int Strength = 25;
}

public enum UnitType
{
    PlayerDefender,
    PlayerGenerator,
    EnemyAttacker
}