using UnityEngine;

public class LetterTile : Tile {

    public LetterTile SetScale(int scale)
    {
        Vector2 newScale = transform.localScale;
        newScale.x = scale;
        newScale.y = scale;
        transform.localScale = newScale;

        return this;
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
