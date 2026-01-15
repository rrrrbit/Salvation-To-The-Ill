using UnityEngine;

public class NPC : ENTITY
{

    public GameObject currentTarget;

    public Vector3 targetPosD;
    public override void Start()
    {
        base.Start();
        MGR.npc.npcs.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        targetPosD = currentTarget.transform.position - transform.position;
    }

    private void OnDestroy()
    {
        MGR.npc.npcs.Remove(this);
    }
}
