using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Alphabet : MonoBehaviour {

    public List<Letter> alphabet;
    Dictionary<string, Letter> lookup;
    
    // Use this for initialization
    void Start () {
        lookup = new Dictionary<string, Letter>();
        foreach(Letter l in alphabet)
        {
            if (lookup.ContainsKey(l.Name))
            {
                Debug.Log("Already contains definition for character: " + l.Name);
            }
            else lookup.Add(l.Name, l);
        }
	}	

    public Letter getLetter(string letter)
    {
        return lookup[letter.ToUpper()];
    }
}
