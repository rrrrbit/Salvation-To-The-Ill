using UnityEngine;

public class NPC : ENTITY
{

    public GameObject currentTarget;
    public override void Start()
    {
        base.Start();
        MGR.npc.npcs.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        MGR.npc.npcs.Remove(this);
    }
}
