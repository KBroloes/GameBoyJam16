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

    [Header("Spawnable Units")]
    public List<Unit> PlayerUnits;

    int menuOffset = 3;
    bool gameOver;
    Currency currency;

    void Init()
    {
        PlayerUnitMap = new Unit[xUnits, yUnits];
        currency = GetComponent<Currency>();
    }

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
        if (!MenuScreen.instance.IsActive)
            currency.DrawUIElement(new Vector2(1, 1.5f));

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
            currency.EraseUIElement();
        }
    }

    void FixedUpdate()
    {
        if(timeLeft > 0 )
        {
            timeLeft -= Time.fixedDeltaTime * timePassRatePerSecond;
            timeLeft = Mathf.Clamp(timeLeft, 0f, totalTime);
        }
    }

    public void EndGame()
    {
        MenuScreen.instance.ShowMenu("You Lose!");
        currency.EraseUIElement();
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
                if(unit.Cost > currency.Get())
                {
                    //TODO: Display help text and current currency
                    return;
                }
                else if (CanSpawnUnit(location))
                {
                    currency.Spend(unit.Cost);

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
