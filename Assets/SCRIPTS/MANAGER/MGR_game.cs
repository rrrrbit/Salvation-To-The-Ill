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

    public List<WORLD_pickupSpawn> pickupSpawns;
    public List<Transform> spawns;


    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        baseDifficulty = (Mathf.Sqrt(timer / 30 + 0.25f) - 0.5f);
        difficulty = gameDifficulty * baseDifficulty;
    }

    void StartNewWave()
    {
        wave++;
        waveTimer = 60 + baseDifficulty;
    }

}
