using System;
using UnityEditor;
using UnityEngine;

public class WORLD_npcSpawn : MonoBehaviour
{
    public int leftToSpawn;

    public float timer;
    public float spawnInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MGR.game.spawns.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        timer = Mathf.Max(timer, timer - Time.deltaTime);

        if(timer <= 0)
        {
            timer = spawnInterval;
            if(leftToSpawn > 0)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        MGR.entities.RandomNPC().transform.position = transform.position;
        leftToSpawn--;
    }
}
