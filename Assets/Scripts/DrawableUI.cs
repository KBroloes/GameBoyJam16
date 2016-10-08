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
    public Tile icon;

    [Header("Debug")]
    public int tileLength;

    Word drawable;
    List<Tile> backgroundTiles;
    Tile activeIcon;
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
        CalculatePositionOffset();

        if (background != null)
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
        if(icon != null)
        {
            activeIcon = Instantiate(icon);
            // Place icon in front of UI text, regardless of background;
            Vector2 iconPosition = position;
            iconPosition.x -= 0.5f * scale - xOffset;
            iconPosition.y += yOffset;
            activeIcon.transform.position = iconPosition;

            // Resize icon to scale
            activeIcon.transform.localScale = new Vector2(scale, scale);
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
        if(activeIcon != null)
        {
            Destroy(activeIcon.gameObject);
        }
    }

    float xOffset = 0f;
    float yOffset = 0f;
    public void CalculatePositionOffset()
    {
        switch(mask)
        {
            case Word.PositionMask.TopLeft:
                xOffset = 0;
                yOffset = 0f;
                break;
            case Word.PositionMask.TopRight:
                xOffset = 0.5f;
                yOffset = 0f;
                break;
            case Word.PositionMask.BottomLeft:
                xOffset = 0;
                yOffset = -0.5f;
                break;
            case Word.PositionMask.BottomRight:
                xOffset = 0.5f;
                yOffset = -0.5f;
                break;
        }
    }

    
    public int GetTileLength()
    {
        return Mathf.CeilToInt(drawable.GetLength() * 0.5f * scale + xOffset);
    }
}
