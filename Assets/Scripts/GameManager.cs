using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Header("Board")]
    public int xUnits = 10;
    public int yUnits = 5;
    public Unit[,] PlayerUnitMap;

    [Header("Time")]
    public int timeLeft = 100;
    public int timePassRatePerSecond = 1;

    [Header("Currency")]
    public int passiveGeneration = 10;
    public int activeGeneration = 0;

    [Header("Spawnable Units")]
    public List<Unit> PlayerUnits;
    public List<Unit> EnemyUnits;

    int menuOffset = 3;

    void Start () {
	    if(instance != null)
        {
            Debug.Log("Found existing GameManager instance, destroying this one");
            Destroy(this);
        }
        instance = this;

        Init();
	}

    void Init()
    {
        PlayerUnitMap = new Unit[xUnits, yUnits];
    }

    public void SpawnUnit(UnitType type, Vector2 location)
    {
        Vector2 relativeLocation = GetBoardRelativeCoordinates(location);
        int x = (int)relativeLocation.x;
        int y = (int)relativeLocation.y;

        foreach (Unit unit in PlayerUnits)
        {
            if(unit.unitType == type)
            {

                if (PlayerUnitMap[x,y] == null)
                {
                    Unit instantiated = Instantiate(unit);
                    instantiated.transform.position = location;
                    PlayerUnitMap[x, y] = instantiated;
                }
                break;
            }
        }
    }

    Vector2 GetBoardRelativeCoordinates(Vector2 location)
    {
        Vector2 relativeCoords = location;

        relativeCoords.y = relativeCoords.y - menuOffset;

        if (relativeCoords.y < 0) relativeCoords.y = 0;
        return relativeCoords;
    }

    public Unit GetUnitAt(int x, int y)
    {
        y = y + menuOffset;
        return PlayerUnitMap[x, y];
    }
	
	void Update () {
	
	}
}
