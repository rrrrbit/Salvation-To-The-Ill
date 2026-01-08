using UnityEngine;
using static UnityEngine.UI.Image;

public class WEAPON_singleShot : UseBehaviour
{
	public float costAmmo;
	public GameObject bullet;
	public float shootInterval;
	public float speed;

	public override bool TryUse()
	{
		PLYR.shoot.shootTimer = shootInterval;
		if (PLYR.stats.ammo < costAmmo) return false;
		PLYR.stats.ammo -= costAmmo;
		GameObject thisBullet = Instantiate(bullet);
		thisBullet.transform.position = PLYR.shoot.origin.position;
		thisBullet.transform.forward = PLYR.shoot.origin.forward;
		if (thisBullet.GetComponent<Rigidbody>() != null)
		{
			thisBullet.GetComponent<Rigidbody>().AddForce(speed * PLYR.shoot.origin.forward, ForceMode.VelocityChange);
		}
		return true;
	}
}
