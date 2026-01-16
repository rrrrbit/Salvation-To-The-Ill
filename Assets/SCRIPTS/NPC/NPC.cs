
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class NPC : ENTITY
{

    public GameObject currentTarget;

    public Vector3 targetPosD;
    public float useRange;

    public LayerMask walls;

    public override void Update()
    {
        base.Update();
        RecalculateTarget();
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
        if (currentTarget)
        {
            targetPosD = currentTarget.transform.position - transform.position;
            ((NPC_movement)movement).sufficientRange = useRange - 1;
            item.use = targetPosD.sqrMagnitude <= useRange * useRange;
        }
        else
        {
            targetPosD = transform.position;
            item.use = false;
        }
    }

    void RecalculateTarget()
    {
        var isHealingWeapon = item.GetCurrent() && item.GetCurrent().TryGetComponent(out WEAPON w) && w.stats.heal;
        List<ENTITY> enemies = MGR.entities.entities.Where(x => (x.team == team) == isHealingWeapon).ToList();
        if (enemies.Count <= 0)
        {
            currentTarget = null;
            return;
        }
        List<ENTITY> lineOfSight = enemies.Where(x => !Physics.Linecast(transform.position, x.transform.position, walls)).ToList();
        if(lineOfSight.Count > 0)
        {
            currentTarget = lineOfSight.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).First().obj;
        }
        else
        {
            currentTarget = enemies.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).First().obj;
        }
    }
}
