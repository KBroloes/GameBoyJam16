using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

    [Header("UI")]
    public DrawableUI drawableWave;

    [Header("Waves")]
    public List<Wave> Waves;
    public bool finalWave;

    [Header("Debug only")]
    public int nextWave = 0;
    Wave currentWave = null;
    public bool beginWaves;

    float timeLeft;
    void Start()
    {
        float totalTime = 0f;
        foreach(Wave wave in Waves)
        {
            totalTime += wave.duration;
        }
        timeLeft = totalTime;
        beginWaves = false;
    }
    
    float timer;
    float waveTime;
    public void BeginWaves()
    {
        beginWaves = true;
        BeginNextWave();
    }

    void FixedUpdate()
    {
        waveTime += Time.deltaTime;
        if(currentWave != null)
        {
            if(waveTime >= timer)
            {
                BeginNextWave();
                waveTime = 0f;
            }
            EnemySpawnTimer();
        }
    }

    void Update()
    {
        drawableWave.UIValue = " 0" + nextWave;

        if (currentWave != null)
        {
            SpawnEnemy();
        }
    }

    void BeginNextWave()
    {
        if(finalWave)
        {
            currentWave = null;
        } else
        {
            currentWave = Waves[nextWave];
            nextWave++;

            timer = currentWave.duration;

            // Reached end of waves?
            finalWave = nextWave == Waves.Count;
        }
    }

    float spawnTimer;
    bool canSpawnEnemy;
    void EnemySpawnTimer()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentWave.spawnRate)
        {
            spawnTimer = 0f;
            canSpawnEnemy = true;
        }
    }

    void SpawnEnemy()
    {
        if (canSpawnEnemy)
        {
            int lane = Random.Range(currentWave.minLane, currentWave.maxLane);
            int enemyIndex = Random.Range(0, currentWave.EnemyUnits.Count);

            Enemy enemy = currentWave.EnemyUnits[enemyIndex];
            GameManager.instance.SpawnEnemy(enemy, new Vector2(10, lane));
            AIDirector.instance.EnemiesLeft++;

            canSpawnEnemy = false;
        }
    }

    public float GetTotalTime()
    {
        float totalTime = 0f;
        for (int i = 0; i < Waves.Count; i++)
        {
            totalTime += Waves[i].duration;
        }

        return totalTime;
    }
}
