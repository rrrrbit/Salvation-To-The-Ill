using System.Collections.Generic;
using UnityEngine;

public class MGR_npc : MonoBehaviour
{
    public List<NPC> npcs;
    public float recalcPathsTime = .1f;
    public float recalcPathsTimer = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcs = new();
    }

    // Update is called once per frame
    void Update()
    {
        recalcPathsTimer -= Time.deltaTime;
        if(recalcPathsTimer < 0)
        {
            recalcPathsTimer = recalcPathsTime;
            foreach(NPC npc in npcs)
            {
                ((NPC_movement)npc.movement).RecalcPath();
            }
        }
    }
}
