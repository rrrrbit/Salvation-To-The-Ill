using System.Collections.Generic;
using UnityEngine;

public class MGR_game : MonoBehaviour
{
    public float baseDifficulty;
    public float gameDifficulty;
    public float difficulty;

    public float timer;

    public float replenishTimer;
    public float waveTimer;
    public int wave;
    public int spawnCount;

    public List<WORLD_pickupSpawn> pickupSpawns;
    public List<Transform> spawns;


    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        waveTimer -= Time.deltaTime;
        baseDifficulty = (Mathf.Sqrt(timer / 30 + 0.25f) - 0.5f);
        difficulty = gameDifficulty * baseDifficulty;

        if(waveTimer < 0)
        {
            StartNewWave();
        }
    }

    void StartNewWave()
    {
        wave++;
        waveTimer = 15 + baseDifficulty;
        spawnCount = Mathf.FloorToInt(10 * (baseDifficulty+1));

        for (int i = 0; i < spawnCount; i++)
        {
            MGR.entities.RandomNPC().transform.position = spawns[Random.Range(0, spawns.Count - 1)].position;
        }

        foreach(var p in pickupSpawns)
        {
            p.Replenish();
        }
    }

}
