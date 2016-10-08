using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawableUI : MonoBehaviour {
    [Header("DrawableUI")]
    public string UIValue;
    public Tile background;
    public int scale = 1;

    [Header("Debug")]
    public int tileLength;

    Word drawable;
    List<Tile> backgroundTiles;
    void Start()
    {
        backgroundTiles = new List<Tile>();
    }

    public void DrawUIElement(Vector2 position)
    {
        EraseUIElement();
        
        //TODO: Maybe use a different writer.
        drawable = MenuScreen.instance.writer.WriteWord(UIValue, scale);
        drawable.transform.position = position;

        if(background != null)
        {
            tileLength = GetTileLength();
            for (int i = 0; i < tileLength; i++)
            {
                Vector2 backgroundPos = position;
                backgroundPos.x += i;

                Tile bgTile = Instantiate(background);
                bgTile.transform.position = backgroundPos;
                backgroundTiles.Add(bgTile);
            }
        }
    }
    
    public void EraseUIElement()
    {
        if (drawable != null)
        {
            drawable.Erase();
        }
        foreach(Tile t in backgroundTiles)
        {
            Destroy(t.gameObject);
        }
        backgroundTiles.Clear();
    }

    public int GetTileLength()
    {
        return Mathf.CeilToInt(drawable.GetLength() * 0.5f * scale);
    }
}
