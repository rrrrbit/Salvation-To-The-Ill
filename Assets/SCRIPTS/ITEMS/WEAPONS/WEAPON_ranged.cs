using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.UI.Image;

public class WEAPON_ranged : WEAPON
{
	public GameObject bullet;

	public override bool TryUse(ENTITY user, ENTITY recipient)
	{
		if(!Consume(user)) return false;

		var spread = stats.spread[Quality()];
		var speed = stats.bulletSpeed[Quality()];
		var bullets = stats.bullets[Quality()];

		AttackGroup group = new();

		for (int i = 0; i < bullets; i++)
		{
			Vector2 angle = Random.insideUnitCircle.Scaled(new(spread.x / 2, spread.y / 2));
			GameObject thisBullet = Instantiate(bullet, user.inventory.useOrigin.position, Quaternion.LookRotation(Quaternion.AngleAxis(angle.x, user.inventory.useOrigin.up) * Quaternion.AngleAxis(angle.y, user.inventory.useOrigin.right) * user.inventory.useOrigin.forward));

			if (thisBullet.TryGetComponent(out Rigidbody r))
			{
				r.AddForce(speed * thisBullet.transform.forward, ForceMode.VelocityChange);
			}
			if(thisBullet.TryGetComponent(out OBJ_Projectile p))
			{
                p.group = group;
				p.origin = user.gameObject;
				p.originStats = stats;
				p.originQuality = Quality();
            }
		}

		user.look.look += user.look.displacement.Scaled(new(1,-1));
		var recoil = Vector2.zero;
		if (user.inventory.aim)
		{
			recoil = new(
				Random.Range(stats.aimRecoilRange[Quality()].x, stats.aimRecoilRange[Quality()].z),
				Random.Range(stats.aimRecoilRange[Quality()].y, stats.aimRecoilRange[Quality()].w)
				);
		}
		else
		{
			recoil = new(
				Random.Range(stats.recoilRange[Quality()].x, stats.recoilRange[Quality()].z),
				Random.Range(stats.recoilRange[Quality()].y, stats.recoilRange[Quality()].w)
				);
		}
		user.look.displacement = recoil;

		
		return true;
	}
}
