using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Writer : MonoBehaviour {

    Alphabet alphabet;
    public LetterTile letterPrefab;
    public Word wordPrefab;

	// Use this for initialization
	void Start () {
        alphabet = GetComponent<Alphabet>();

        if(alphabet == null)
        {
            Debug.Log("Alphabet required to write, destroying the writer");
            Destroy(gameObject);
        }
	}

    public List<Word> Write(string garbleflarg)
    {
        string[] split = garbleflarg.Split(' ');
        List<Word> words = new List<Word>();

        // For now, one line per word.
        for(int lineNo = 0; lineNo < split.Length; lineNo++)
        {
            words.Add(WriteWord(split[lineNo]));
        }

        return words;        
    }

    public Word WriteWord(string word)
    {
        List<LetterTile> letters = new List<LetterTile>();

        for(int i = 0; i < word.Length; i++)
        {
            char c = word[i];
            Letter l = alphabet.getLetter(c.ToString());
            LetterTile newLetter = Instantiate(letterPrefab);

            newLetter.SetScale(2);
            newLetter.SetSprite(l.sprite);
            newLetter.transform.localPosition = new Vector2(i, 0);

            letters.Add(newLetter);
        }
        Word newWord = Instantiate(wordPrefab);
        newWord.Add(letters);

        return newWord;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
