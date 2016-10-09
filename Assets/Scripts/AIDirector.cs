using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIDirector : MonoBehaviour {
    [Header("Delay before first wave")]
    public int spawnDelay = 5;

    [Header("Enemies spawned")]
    public int EnemiesLeft = 0;

    [Header("Debug only")]
    public float deltaTime;

    [Header("Dependencies")]
    public WaveManager waveManager;
    public TimeManager timeManager;

    public static AIDirector instance;
    void Start()
    {
        if(instance != null)
        {
            Debug.Log("Already have an AI director, deleting this one");
            Destroy(this);
        }
        instance = this;
    }

    bool wavesStarted;
    void FixedUpdate()
    {
        deltaTime += Time.fixedDeltaTime;

        if (!wavesStarted)
        {
            if (deltaTime >= spawnDelay)
            {
                waveManager.BeginWaves();
                wavesStarted = true;
                deltaTime = 0;
            }
        }
    }

    void Update()
    {
        UpdateTimeLeft();
    }

    public bool GameEnd()
    {
        return waveManager.finalWave && EnemiesLeft == 0 && timeLeft <= 0;
    }

    float timeLeft;
    void UpdateTimeLeft()
    {
        if(!wavesStarted)
        {
            timeLeft = waveManager.GetTotalTime() + spawnDelay - deltaTime;
        } else
        {
            timeLeft = waveManager.GetTotalTime() - deltaTime;
        }
        timeLeft = Mathf.Clamp(timeLeft, 0, 999);

        timeManager.Set(timeLeft);
    }
}
