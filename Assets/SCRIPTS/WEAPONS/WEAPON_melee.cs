using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Image;

public class WEAPON_melee : UseBehaviour
{
	public float shootInterval;
	public float range;
	public LayerMask target;
	public LayerMask collideWith;

	public AttackStats stats;

	public override bool TryUse()
	{
		PLYR.item.shootTimer = shootInterval;
		Debug.DrawRay(PLYR.item.useOrigin.position, PLYR.item.useOrigin.forward * range);
		RaycastHit hit;
		if(Physics.Raycast(new Ray(PLYR.item.useOrigin.position, PLYR.item.useOrigin.forward), out hit, range, collideWith))
		{
			print("hit");
			if ((target & (1 << hit.collider.gameObject.layer)) != 0)
            {
                if (hit.collider.gameObject.GetComponent<IAttackable>() != null)
                {
                    AttackContext ctx = new();
                    ctx.attacker = gameObject;
                    ctx.baseDmg = Random.Range(stats.minDmg, stats.maxDmg);
                    hit.collider.gameObject.GetComponent<IAttackable>().Attack(ctx);


                    MGR.vfx.DmgText(ctx, hit.point, false);
                }
            }
        }
		
		
		return true;
	}
}
