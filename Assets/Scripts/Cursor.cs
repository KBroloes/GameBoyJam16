using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

    public Sprite cursor;
    public Sprite badCursor;

    [Header("Lanes")]
    public int minLaneX;
    public int minLaneY = 3;
    public int maxLaneY = 7;
    public int maxLaneX = 9;

    [Header("Menu")]
    public bool inMenu;
    public int minMenuX = 1;
    public int maxMenuX = 8;
    public int menuY = 1;
    
    public Vector2 lastKnownPosition;
    public Vector2 menuPosition;

    Sprite currentCursor;
    void Start () {
        currentCursor = transform.GetComponent<SpriteRenderer>().sprite;
        currentCursor = cursor;
        transform.position = new Vector2(minLaneX, minLaneY);
	}
	
	void Update () {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveY(1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveY(-1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveX(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveX(1);
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(!inMenu)
            {
                lastKnownPosition = transform.position;
                GoToMenu();
            } else
            {
                transform.position = lastKnownPosition;
            }
            inMenu = !inMenu;
        }

        if(Input.GetKeyDown(KeyCode.A) && inMenu)
        {
            menuPosition = transform.position;
            transform.position = lastKnownPosition;

            UnitType unit = GetSelection(menuPosition);
            
            GameManager.instance.SpawnUnit(unit, lastKnownPosition);

            inMenu = !inMenu;
        }
    }

    void GoToMenu()
    {
        transform.position = menuPosition = new Vector2(minMenuX, menuY);
    }

    void MoveY(int toMove)
    {
        Vector2 pos = transform.position;

        if(!inMenu)
        {
            pos.y = Mathf.Clamp(pos.y + toMove, minLaneY, maxLaneY);
        }
        transform.position = pos;
    }

    void MoveX(int toMove)
    {
        Vector2 pos = transform.position;

        if(!inMenu)
        {
            pos.x = Mathf.Clamp(pos.x + toMove, minLaneX, maxLaneX);
        } else
        {
            pos.x = Mathf.Clamp(pos.x + toMove, minMenuX, maxMenuX);
        }
        transform.position = pos;
    }

    UnitType GetSelection(Vector2 menuCoordinates)
    {
        int selection = (int)menuCoordinates.x - 1;
        
        return (UnitType)selection;
    }
}
