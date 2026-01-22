using Unity.VisualScripting;
using UnityEngine;

public class WORLD_pickupSpawn : MonoBehaviour
{
    public GameObject currentPickup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MGR.game.pickupSpawns.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replenish()
    {
        if (!currentPickup || currentPickup.IsDestroyed())
        {
            currentPickup = MGR.entities.RandomPickup();
            currentPickup.transform.position = transform.position;
        }
    }
}
