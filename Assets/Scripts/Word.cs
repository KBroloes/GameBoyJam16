using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Word : MonoBehaviour {

    public List<LetterTile> Letters;
    
    public void Add(List<LetterTile> letters) {
        Letters.AddRange(letters);
        foreach(LetterTile l in letters)
        {
            l.transform.parent = transform;
        }
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
}
