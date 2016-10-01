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
    
    void Start () {
        Screen.SetResolution(width, height, false);
        xTiles = width / tileRoot;
        yTiles = height / tileRoot;

        tileMap = new TileType[xTiles, yTiles];

        for(int x = 0; x < xTiles; x++)
        {
            for(int y = 0; y < yTiles; y++)
            {
                tileMap[x, y] = TileType.Grass;
            }
        }
        RenderWorld(tileMap);
	}

    void RenderWorld(TileType[,] world)
    {
        for(int x = 0; x < world.GetLength(0); x++)
        {
            for(int y = 0; y < world.GetLength(1); y++)
            {
                TileType t = world[x, y];
                foreach(Tile tile in Tiles)
                {
                    if(tile.TileType == t)
                    {
                        GameObject g = Instantiate(tile.GameObject);
                        g.transform.position = getWorldPos(x, y);
                        break;
                    }
                }
            }
        }
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