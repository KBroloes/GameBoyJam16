using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public List<Sound> Sounds;

    List<Sound> queue;
    AudioSource source;

    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Found existing SoundManager instance, destroying this one");
            Destroy(this);
        }
        instance = this;
        queue = new List<Sound>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(queue.Count >= 1)
        {
            Sound toPlay = queue[0];
            queue.RemoveAt(0);
            AudioClip randomClip = GetClip(toPlay.clips);

            if(!source.isPlaying)
                source.PlayOneShot(randomClip);
        }
    }

    public void PlayOnce(SoundType type)
    {
        Sound sound = null;
        foreach(Sound s in Sounds)
        {
            if(s.type == type)
            {
                sound = s;
            }
        }

        if(sound != null)
        {
            queue.Add(sound);
        }
    }

    public AudioClip GetClip(List<AudioClip> clips)
    {
        int rand = Random.Range(0, clips.Count - 1);
        return clips[rand];
    }
}

[System.Serializable]
public class Sound
{
    public SoundType type;
    public List<AudioClip> clips;
}

public enum SoundType
{
    Hit,
    Throw
}