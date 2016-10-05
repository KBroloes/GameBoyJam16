using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuScreen : MonoBehaviour {

    public bool IsActive = false;
    public static MenuScreen instance;
    public Writer writer;
    
    List<GameObject> tiles;
    List<Word> words;

    public int width = 10;
    public int height = 9;

    public List<MenuTile> MenuTiles;

    void Start () {
        if(instance != null)
        {
            Debug.Log("A MenuScreen already exists, destroying this one");
            Destroy(this);
        } else
        {
            instance = this;
            tiles = new List<GameObject>();
        }
	}

    void Update()
    {
        if(IsActive)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }

    public void ShowMenu(string message)
    {
        RenderMenu();

        int scale = 2;
        words = writer.Write(message, scale);

        // Account for coordinate system
        words.Reverse();

        float yPos = GetCenteredStartYPos(words, scale);

        foreach (Word word in words)
        {
            int xPos = GetCenteredStartXPos(word, scale);
            Vector2 position = new Vector2(xPos, yPos);
            word.transform.position = position;

            yPos += scale * 0.5f;
        }
    }

    int wordsPerTile = 2;
    public int GetCenteredStartYPos(List<Word> words, int scale)
    {
        int wordcount = words.Count;
        wordcount /= wordsPerTile * scale;

        int startPosY = 0;
        if (wordcount > height)
        {
            // Whoops
        }
        else
        {
            int margin = 2;
            startPosY = (height - wordcount) / margin;
        }

        return startPosY;
    }

    int lettersPerTile = 2;
    public int GetCenteredStartXPos(Word word, int scale)
    {
        int tilesRequired = word.Letters.Count;
        tilesRequired /= lettersPerTile / scale;

        int startPosX = 0;
        if(tilesRequired > width)
        {
            // Whoops
        } else
        {
            int margin = 2;
            startPosX = (width - tilesRequired) / margin;
        }

        return startPosX;
    }
    
    void RenderMenu()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                AddMenuTile(x, y);
            }
        }
        IsActive = true;
    }

    public void CloseMenu()
    {
        foreach(GameObject g in tiles)
        {
            Destroy(g);
        }
        tiles.Clear();
        foreach (Word word in words)
        {
            word.Erase();
        }
        words.Clear();
        IsActive = false;
    }

    void AddMenuTile(int x, int y)
    {
        Tile tile;
        // Scan from bottom to top
        if(y == 0)
        {
            if(x == 0)
                tile = GetMenuTile(TilePlacement.BottomLeftCorner);
            else if(x == width -1)
                tile = GetMenuTile(TilePlacement.BottomRightCorner);
            else
                tile = GetMenuTile(TilePlacement.Bottom);
        }
        else if(y == height -1)
        {
            if(x == 0)
                tile = GetMenuTile(TilePlacement.TopLeftCorner);
            else if(x == width -1)
                tile = GetMenuTile(TilePlacement.TopRightCorner);
            else 
               tile = GetMenuTile(TilePlacement.Top);
        }
        else
        {
            if(x == 0)
                tile = GetMenuTile(TilePlacement.LeftSide);
            else if(x == width -1)
                tile = GetMenuTile(TilePlacement.RightSide);
            else
                tile = GetMenuTile(TilePlacement.Center);
        }

        AddTile(x, y, tile);
    }

    void AddTile(int x, int y, Tile tile)
    {
        GameObject g = Instantiate(tile.GameObject);
        g.transform.position = new Vector2(x, y);
        SpriteRenderer s = g.GetComponent<SpriteRenderer>();
        s.sortingOrder = 10;
        tiles.Add(g);
    }

    public MenuTile GetMenuTile(TilePlacement placement)
    {
        foreach (MenuTile tile in MenuTiles)
        {
            if (tile.Placement == placement)
            {
                return tile;
            }
        }
        return null;
    }
}
