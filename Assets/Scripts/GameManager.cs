using UnityEngine;
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
    public int totalTime = 60;
    public int timePassRatePerSecond = 1;
    public float timeLeft;

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
        timeLeft = totalTime;

        Init();
	}

    void Update()
    {
        if(!MenuScreen.instance.IsActive)
            DrawCurrency();

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

        if (timeLeft <= 0 && AIDirector.instance.enemies == 0)
        {
            WinGame();
            currentCurrency.Erase();
        }

        if(addCurrency)
        {
            currency += activeGeneration + passiveGeneration;
            addCurrency = false;
        }
    }

    Word currentCurrency;
    void DrawCurrency()
    {
        if(currentCurrency != null)
        {
            currentCurrency.Erase();
        }
        currentCurrency = MenuScreen.instance.writer.WriteWord(currency.ToString(), 1);
        currentCurrency.transform.position = new Vector2(1, 1.5f);
    }

    float currencyTimer = 0f;
    bool addCurrency;
    void FixedUpdate()
    {
        if(timeLeft > 0 )
        {
            timeLeft -= Time.fixedDeltaTime * timePassRatePerSecond;
            timeLeft = Mathf.Clamp(timeLeft, 0f, totalTime);

            currencyTimer += Time.fixedDeltaTime;
            if (currencyTimer >= 1f)
            {
                currencyTimer -= 1f;
                addCurrency = true;
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
    public void WinGame()
    {
        MenuScreen.instance.ShowMenu("You Win!");
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
                if(unit.Cost > currency)
                {
                    //TODO: Display help text and current currency
                    return;
                }
                else if (CanSpawnUnit(location))
                {
                    currency -= unit.Cost;

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

    public float GetTimeLeft()
    {
        return timeLeft;
    }
}
