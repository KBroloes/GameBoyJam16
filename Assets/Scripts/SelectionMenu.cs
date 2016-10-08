using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionMenu : MonoBehaviour {
    [Header("Menu Dimensions")]
    public int width = 10;
    public int height = 3;

    [Header("Tiles")]
    public List<MenuTile> MenuTiles;
    public List<SelectionItem> MenuItems;

    SelectionItem[,] selectionItems;
    void Start()
    {
        selectionItems = new SelectionItem[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                AddMenuTile(x, y);
            }
        }
        AddMenuItems();
    }

    public SelectionItem GetSelectionItem(Vector2 position)
    {
        if (position.x >= selectionItems.GetLength(0) || position.y >= selectionItems.GetLength(1))
            return null;

        return selectionItems[(int)position.x, (int)position.y];
    }

    MenuTile GetMenuTile(TilePlacement placement)
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

    void AddTile(int x, int y, Tile tile)
    {
        GameObject g = Instantiate(tile.GameObject);
        g.transform.position = new Vector2(x, y);
    }

    void AddMenuItems()
    {
        int x = 1;
        int y = 1;
        // Hardcoded to one row for now

        foreach (SelectionItem item in MenuItems)
        {
            if (item.selectionBackground != null)
            {
                AddTile(x, y, item.selectionBackground);
            }

            AddTile(x, y, item.selectionTile);
            selectionItems[x, y] = item;
            x++;
        }
    }

    void AddMenuTile(int x, int y)
    {
        Tile tile;
        // Check Left Border
        if (x == 0)
        {
            switch (y)
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
        else if (x == width - 1)
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
        else if (y == 0)
        {
            tile = GetMenuTile(TilePlacement.Bottom);
        }
        // Check Top
        else if (y == height - 1)
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
}
