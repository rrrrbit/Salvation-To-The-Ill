
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
	public float defaultStopRange;

    public LayerMask walls;

    public override void Update()
    {
        base.Update();
        RecalculateTarget();

        if(team == Teams.HUMAN)
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        if (inventory.GetCurrent())
        {
            if (inventory.GetCurrent().TryGetComponent(out WEAPON w))
            {
                useRange = w.stats.effectiveRange[w.Quality()];
            }
            else
            {
                useRange = inventory.GetCurrent().defaultRange;
            }
		}
		else
		{
			useRange = defaultStopRange;
		}
		if (currentTarget)
		{
			targetPosD = currentTarget.transform.position - transform.position;
			((NPC_movement)movement).sufficientRange = useRange - 1;
			inventory.use = targetPosD.sqrMagnitude <= useRange * useRange;
		}
		else
		{
			targetPosD = Vector3.zero;
			inventory.use = false;
		}
    }

    void RecalculateTarget()
    {
        var isHealingWeapon = inventory.GetCurrent() && inventory.GetCurrent().TryGetComponent(out WEAPON w) && w.stats.heal;
        List<ENTITY> targets = new();
        if (isHealingWeapon)
        {
            targets = MGR.entities.entities.ToList();
        }
        else
        {
            targets = MGR.entities.entities.Where(x => x.team != team).ToList();
        }
        targets.Remove(this);
        if (targets.Count <= 0)
        {
            currentTarget = null;
            return;
        }
        List<ENTITY> lineOfSight = targets.Where(x => !Physics.Linecast(transform.position, x.transform.position, walls)).ToList();
        if(lineOfSight.Count > 0)
        {
            currentTarget = lineOfSight.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).First().obj;
        }
        else
        {
            currentTarget = targets.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).First().obj;
        }
    }
}
