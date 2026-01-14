using UnityEngine;

public class NPC_item : Item
{
    public float useRange;

    public override void Update()
    {
        use = (((NPC)entity).currentTarget.transform.position - transform.position).sqrMagnitude <= useRange * useRange;
        base.Update();
    }
}
