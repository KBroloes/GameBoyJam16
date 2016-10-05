using UnityEngine;
using System.Collections;

public class LetterTile : Tile {

    public int scale = 1;

    public LetterTile SetScale(int scale)
    {
        Vector2 newScale = transform.localScale;
        newScale.x = scale;
        newScale.y = scale;
        transform.localScale = newScale;

        return this;
    }

    public void SetPosition(int mask)
    {
        Vector2 position = transform.localPosition;
        switch(mask)
        {
            case 0:
            default:
                position = new Vector2(0, 0);
                break;
            case 1:
                position = new Vector2(0.5f, 0);
                break;
            case 2:
                position = new Vector2(0, 0.5f);
                break;
            case 3:
                position = new Vector2(0.5f, 0.5f);
                break;
        }
        transform.localPosition = position;
    }

    public void SetSprite(Sprite s)
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        spr.sprite = s;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
