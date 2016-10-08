using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Header("Board")]
    public int xUnits = 10;
    public int yUnits = 5;
    public Unit[,] PlayerUnitMap;
    
    int menuOffset = 3;
    bool gameOver;

    [Header("Dependencies")]
    public Currency currency;
    public TimeManager time;
    public GameUI gameUI;
    public SelectionMenu selectionMenu;

    void Init()
    {
        PlayerUnitMap = new Unit[xUnits, yUnits];
    }

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

        if (time.Get() <= 0 && AIDirector.instance.enemies == 0)
        {
            WinGame();
        }
    }    

    public void EndGame()
    {
        if (!MenuScreen.instance.IsActive)
        {
            MenuScreen.instance.ShowMenu("You Lose!");
        }
        gameOver = true;
    }
    public void WinGame()
    {
        if (!MenuScreen.instance.IsActive)
        {
            MenuScreen.instance.ShowMenu("You Win!");
        }
        gameOver = true;
    }

    // Assumes lane-centric coordinates
    public void SpawnEnemy(Enemy enemy, Vector2 location)
    {
        GameObject instantiated = Instantiate(enemy.unitPrefab);
        
        instantiated.transform.position = GetWorldVector(location);
    }

    public void SpawnUnit(Vector2 menuPosition, Vector2 spawnLocation)
    {
        Coord coord = GetBoardRelativeCoordinates(spawnLocation);
        SelectionItem selection = selectionMenu.GetSelectionItem(menuPosition);

        if(selection != null)
        {
            Unit unit = selection.unit;
            if (selection.Cost > currency.Get())
            {
                //TODO: Display help text/mark cursor
                return;
            }
            else if (CanSpawnUnit(spawnLocation))
            {
                currency.Spend(selection.Cost);

                Unit instantiated = Instantiate(unit);
                instantiated.transform.position = spawnLocation;
                PlayerUnitMap[coord.x, coord.y] = instantiated;
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
        return time.Get();
    }
}
