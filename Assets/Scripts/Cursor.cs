using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

    public Sprite cursor;
    public Sprite badCursor;
    public int maxY;
    public int maxX;

    Sprite currentTexture;
    void Start () {
        currentTexture = transform.GetComponent<SpriteRenderer>().sprite;
        currentTexture = cursor;
	}
	
	void Update () {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveY(1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveY(-1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveX(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveX(1);
        }
    }

    void MoveY(int toMove)
    {
        Vector2 pos = transform.position;

        pos.y = Mathf.Clamp(pos.y + toMove, 0, maxY);
        transform.position = pos;
    }

    void MoveX(int toMove)
    {
        Vector2 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x + toMove, 0, maxX);
        transform.position = pos;
    }
}
