using UnityEngine;

public class NPC : ENTITY
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
