using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap : MonoBehaviour {

    [Header("Dimensions")]
    public int width = 160;
    public int height = 144;
    public int tileRoot = 16;

    [Header("Position")]
    public int xOffset = 0;
    public int yOffset = 0;

    TileType[,] tileMap;
    int yTiles;
    int xTiles;

    [Header("Tiles")]
    public List<Tile> Tiles;
    public List<MenuTile> MenuTiles;
    public List<MenuTile> MenuItems;

    void Start () {
        Screen.SetResolution(800, 600, false);
        xTiles = width / tileRoot;
        yTiles = height / tileRoot;
        
        tileMap = new TileType[xTiles, yTiles];

        for(int x = 0; x < xTiles; x++)
        {
            for(int y = 0; y < yTiles; y++)
            {
                tileMap[x, y] = GetTileType(x, y);
            }
        }
        RenderWorld(tileMap);
	}

    TileType GetTileType(int x, int y)
    {
        // Create 3 row menu
        if(y < 3)
        {
            return TileType.Menu;
        }

        // Menu done, start creating Lanes
        if (y < 8)
        {
            return TileType.Grass;
        }

        return TileType.Fence;
    }

    void RenderWorld(TileType[,] world)
    {
        for(int x = 0; x < world.GetLength(0); x++)
        {
            for(int y = 0; y < world.GetLength(1); y++)
            {
                TileType t = world[x, y];

                if(t == TileType.Menu)
                {
                    AddMenuTile(x, y);
                    continue;
                }

                // figure out which tile to add
                foreach(Tile tile in Tiles)
                {
                    if(tile.TileType == t)
                    {
                        AddTile(x, y, tile);
                        break;
                    }
                }
            }
        }
        AddMenuItems();
    }

    void AddTile(int x, int y, Tile tile)
    {
        GameObject g = Instantiate(tile.GameObject);
        g.transform.position = getWorldPos(x, y);
    }

    void AddMenuTile(int x, int y)
    {
        Tile tile;
        // Check Left Border
        if (x == 0)
        {
            switch(y)
            {
                case 0:
                    tile = GetMenuTile(TilePlacement.BottomLeftCorner);
                    break;
                case 2:
                    tile = GetMenuTile(TilePlacement.TopLeftCorner);
                    break;
                default:
                    tile = GetMenuTile(TilePlacement.LeftSide);
                    break;
            }
        }
        // Check Right Border
        else if (x == (width / tileRoot) -1)
        {
            switch (y)
            {
                case 0:
                    tile = GetMenuTile(TilePlacement.BottomRightCorner);
                    break;
                case 2:
                    tile = GetMenuTile(TilePlacement.TopRightCorner);
                    break;
                default:
                    tile = GetMenuTile(TilePlacement.RightSide);
                    break;
            }
        }
        // Check Bottom
        else if(y == 0)
        {
            tile = GetMenuTile(TilePlacement.Bottom);
        }
        // Check Top
        else if (y == 2)
        {
            tile = GetMenuTile(TilePlacement.Top);
        }
        else
        {
            // Everything else
            tile = GetMenuTile(TilePlacement.Center);
        }

        AddTile(x, y, tile);
    }

    void AddMenuItems()
    {
        int x = 1;
        int y = 1;
        foreach(MenuTile tile in MenuItems)
        {
            AddTile(x, y, tile);
            x++;
        }
    }

    MenuTile GetMenuTile(TilePlacement placement)
    {
        foreach(MenuTile tile in MenuTiles) {
            if(tile.Placement == placement)
            {
                return tile;
            }
        }
        return null;
    }

    Vector2 getWorldPos(int x, int y)
    {
        return new Vector2(x + xOffset, y + yOffset);
    }

    Vector2 getCenter(Vector2 worldPos)
    {
        return new Vector2(worldPos.x + tileRoot / 2, worldPos.y + tileRoot / 2);
    }
	
	void Update () {
	
	}
}

public enum TileType
{
    Grass,
    Fence,
    Menu
}

public enum Spawnables
{
    Generator,
    DefenderWorm
}

public enum Enemies
{
    JumpyBird
}