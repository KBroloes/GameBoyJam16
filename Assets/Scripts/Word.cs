using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Word : MonoBehaviour {

    public List<LetterTile> Letters;
    
    public int GetLength()
    {
        return Letters.Count;
    }
    
    public void Add(List<LetterTile> letters) {
        Letters.AddRange(letters);
        foreach(LetterTile l in letters)
        {
            l.transform.parent = transform;
        }
    }


    public void SetPositionMask(PositionMask mask)
    {
        Vector2 origin = transform.position;
        switch(mask)
        {
            case PositionMask.TopLeft:
            default:
                break;
            case PositionMask.TopRight:
                origin.x += 0.5f;
                break;
            case PositionMask.BottomLeft:
                origin.y -= 0.5f;
                break;
            case PositionMask.BottomRight:
                origin.x += 0.5f;
                origin.y -= 0.5f;
                break;
        }
        transform.position = origin;
    }

    public void Erase()
    {
        foreach(LetterTile l in Letters)
        {
            Destroy(l.gameObject);
        }
        Letters.Clear();
        Destroy(gameObject);
    }

    public enum PositionMask {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
}
