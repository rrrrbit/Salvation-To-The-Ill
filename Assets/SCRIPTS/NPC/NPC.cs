using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class NPC : ENTITY
{

    public GameObject currentTarget;

    public Vector3 targetPosD;
    public float useRange;
    public override void Start()
    {
        base.Start();
        MGR.npc.npcs.Add(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        targetPosD = currentTarget.transform.position - transform.position;
        if (item.GetCurrent())
        {
            if (item.GetCurrent().TryGetComponent(out WEAPON w))
            {
                useRange = w.stats.effectiveRange[w.Quality()];
            }
            else
            {
                useRange = item.GetCurrent().defaultRange;
            }
        }
        item.use = targetPosD.sqrMagnitude <= useRange * useRange;
    }

    private void OnDestroy()
    {
        MGR.npc.npcs.Remove(this);
    }
}
