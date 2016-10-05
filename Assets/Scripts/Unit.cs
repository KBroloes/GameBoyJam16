using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public GameObject unitPrefab;
    public UnitType unitType;

    [Header("Stats")]
    public int Life = 100;
    public int Strength = 25;
    public int Cost = 25;

    void Update()
    {
        if(Life <= 0)
        {
            Destroy(gameObject);
        }
    }
}

public enum UnitType
{
    PlayerDefender,
    PlayerGenerator,
    EnemyAttacker,
    ProjectileMud,
}