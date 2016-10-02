using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public GameObject unitPrefab;
    public UnitType unitType;

    [Header("Stats")]
    public int Life = 100;
    public int Strength = 25;

    void Update()
    {
        if(Life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

public enum UnitType
{
    PlayerDefender,
    PlayerGenerator,
    EnemyAttacker
}