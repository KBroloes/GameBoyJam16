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

    void Start () {
        transform.position = new Vector2(minLaneX, minLaneY);
        menuPosition = new Vector2(minMenuX, menuY);
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
            B();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            A();
        }
    }

    void GoToMenu()
    {
        transform.position = menuPosition;
    }

    public void B()
    {
        if (!inMenu)
        {
            lastKnownPosition = transform.position;
            GoToMenu();
        }
        else
        {
            menuPosition = transform.position;
            transform.position = lastKnownPosition;
        }
        inMenu = !inMenu;
    }

    public void A()
    {
        if (inMenu)
        {
            menuPosition = transform.position;
            transform.position = lastKnownPosition;

            GameManager.instance.SpawnUnit(menuPosition, lastKnownPosition);

            inMenu = !inMenu;
        }
    }

    public void MoveY(int toMove)
    {
        Vector2 pos = transform.position;

        if(!inMenu)
        {
            pos.y = Mathf.Clamp(pos.y + toMove, minLaneY, maxLaneY);
        }
        transform.position = pos;
    }

    public void MoveX(int toMove)
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
}
