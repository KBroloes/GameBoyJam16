using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap : MonoBehaviour {

    [Header("Dimensions")]
    public int width = 160;
    public int height = 144;
    public int tileRoot = 16;
    
    TileType[,] tileMap;
    int yTiles;
    int xTiles;

    [Header("Tiles")]
    public List<Tile> Tiles;

    void Start () {
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
                    // Not this guy's job anymore.
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
    }

    void AddTile(int x, int y, Tile tile)
    {
        GameObject g = Instantiate(tile.GameObject);
        g.transform.position = new Vector2(x, y);
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
    Menu,
    Letter,
    Background
}