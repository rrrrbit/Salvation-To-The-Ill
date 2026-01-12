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

	public override bool TryUse(ENTITY entity)
	{
		entity.item.shootTimer = shootInterval;
		Debug.DrawRay(entity.item.useOrigin.position, entity.item.useOrigin.forward * range);
		RaycastHit hit;
		if(Physics.Raycast(new Ray(entity.item.useOrigin.position, entity.item.useOrigin.forward), out hit, range, collideWith) &&
            target.Contains(hit.collider.gameObject) &&
            hit.collider.gameObject.TryGetComponent(out IAttackable a))
		{
            AttackContext ctx = new()
            {
                attackGroup = null,
                target = hit.collider.gameObject,
                baseDmg = Random.Range(stats.minDmg, stats.maxDmg)
            };
            a.Attack(ctx);
            MGR.vfx.DmgText(ctx, hit.point, false);
        }
		
		
		return true;
	}
}
