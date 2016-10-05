﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Header("Board")]
    public int xUnits = 10;
    public int yUnits = 5;
    public Unit[,] PlayerUnitMap;

    [Header("Time")]
    public int timeLeft = 60;
    public int timePassRatePerSecond = 1;

    [Header("Currency")]
    public int passiveGeneration = 10;
    public int activeGeneration = 0;

    int currency;

    [Header("Spawnable Units")]
    public List<Unit> PlayerUnits;

    int menuOffset = 3;
    bool gameOver;

    void Start () {
	    if(instance != null)
        {
            Debug.Log("Found existing GameManager instance, destroying this one");
            Destroy(this);
        }
        instance = this;

        Init();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!MenuScreen.instance.IsActive)
            {
                MenuScreen.instance.ShowMenu("Paused");
            }
            else if(gameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //TODO: SceneManager.LoadScene("Next Level");
            }
            else
            {
                MenuScreen.instance.CloseMenu();
            }
        }
    }

    void Init()
    {
        PlayerUnitMap = new Unit[xUnits, yUnits];
    }

    public void EndGame()
    {
        MenuScreen.instance.ShowMenu("You Lose!");
        gameOver = true;
    }

    // Assumes lane-centric coordinates
    public void SpawnEnemy(Enemy enemy, Vector2 location)
    {
        GameObject instantiated = Instantiate(enemy.unitPrefab);
        
        instantiated.transform.position = GetWorldVector(location);
    }

    public void SpawnUnit(UnitType type, Vector2 location)
    {
        Coord coord = GetBoardRelativeCoordinates(location);

        foreach (Unit unit in PlayerUnits)
        {
            if(unit.unitType == type)
            {
                if (CanSpawnUnit(location))
                {
                    Unit instantiated = Instantiate(unit);
                    instantiated.transform.position = location;
                    PlayerUnitMap[coord.x, coord.y] = instantiated;
                }
                break;
            }
        }
    }

    Coord GetBoardRelativeCoordinates(Vector2 location)
    {
        Coord relativeCoords = Coord.From(location);

        relativeCoords.y = relativeCoords.y - menuOffset;

        if (relativeCoords.y < 0) relativeCoords.y = 0;
        return relativeCoords;
    }

    Vector2 GetWorldVector(Vector2 BoardLocation)
    {
        return new Vector2(BoardLocation.x, BoardLocation.y + menuOffset);
    }

    public Unit GetUnitAt(int x, int y)
    {
        y = y - menuOffset;
        return PlayerUnitMap[x, y];
    }

    public bool CanSpawnUnit(Vector2 location)
    {
        Coord coord = GetBoardRelativeCoordinates(location);
        return PlayerUnitMap[coord.x, coord.y] == null;
    }
}
