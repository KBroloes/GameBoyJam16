using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawableUI : MonoBehaviour {
    [Header("DrawableUI")]
    public string UIValue;
    public Tile background;
    [Range(1,2)]
    public int scale = 1;

    public Word.PositionMask mask;

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
        drawable.SetPositionMask(mask);

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
        float offset = 0f;
        if (mask == Word.PositionMask.BottomRight || mask == Word.PositionMask.TopRight)
            offset = 0.5f;
        
        return Mathf.CeilToInt(drawable.GetLength() * 0.5f * scale + offset);
    }
}
