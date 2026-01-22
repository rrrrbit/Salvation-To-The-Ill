using System.Collections.Generic;
using UnityEngine;

public class MGR_game : MonoBehaviour
{
    public float difficulty;

    public float timer;

    public float replenishTimer;

    public List<WORLD_pickupSpawn> pickupSpawns;

    void Start()
    {
        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        replenishTimer -= Time.deltaTime;
        if(timer < 0)
        {
            MGR.entities.createRandomNpc();
            timer = 5;
        }

        if (replenishTimer < 0)
        {
            foreach(var p in pickupSpawns)
            {
                p.Replenish();
            }
            replenishTimer = 10f;
        }
    }

}
