using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Image;

public class WEAPON_melee : WEAPON
{
	public LayerMask target;
	public LayerMask collideWith;
	public override bool TryUse(ENTITY user, ENTITY recipient)
	{
		//if(!Consume(entity)) return false;

		var range = stats.effectiveRange[Quality()];
		var dmgRange = stats.dmgRange[Quality()];
		var convRange = stats.conversionRange[Quality()];

        RaycastHit hit;
		if(Physics.Raycast(new Ray(user.inventory.useOrigin.position, user.inventory.useOrigin.forward), out hit, range, collideWith) &&
            target.Contains(hit.collider.gameObject) && ((hit.collider.gameObject.GetComponent<ENTITY>().team != user.team) || stats.heal) &&
            hit.collider.gameObject.TryGetComponent(out IAttackable a))
		{
			AttackContext ctx = new()
			{
				attackGroup = new(),
				target = hit.collider.gameObject,
				baseDmg = Random.Range(dmgRange.x, dmgRange.y),
				baseConv = Random.Range(convRange.x, convRange.y),
				heal = stats.heal,
                attackerTeam = user.team,
				fromPlayer = user == PLYR.player
            };
            a.Attack(ctx);
			Consume(user);
            if (!hit.collider.TryGetComponent<PLYR>(out _)) MGR.vfx.DmgText(ctx, hit.point, false);
        }
		
		return true;
	}
}
