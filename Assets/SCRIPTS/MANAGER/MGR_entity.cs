using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MGR_entity : MonoBehaviour
{
    public GameObject pickupPrefab;
    public GameObject npcPrefab;



    public List<ENTITY> entities = new();
    public float recalcPathsTime = .1f;
    float recalcPathsTimer = 0;
    public LayerMask pickupLayer;
    public LayerMask entityLayers;
    public Transform itemParents;

    // Update is called once per frame
    void Update()
    {
        recalcPathsTimer -= Time.deltaTime;
        if(recalcPathsTimer < 0)
        {
            recalcPathsTimer = recalcPathsTime;
            foreach(NPC npc in entities.Where(x => x.GetType() == typeof(NPC)))
            {
                ((NPC_movement)npc.movement).RecalcPath();
            }
        }
    }
}
