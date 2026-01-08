using UnityEngine;
using static UnityEngine.UI.Image;

public class WEAPON_spreadShot : UseBehaviour
{
	public float costAmmo;
	public GameObject bullet;
	public float shootInterval;
	public float speed;

	public override bool TryUse()
	{
		PLYR.item.shootTimer = shootInterval;
		if (PLYR.stats.ammo < costAmmo) return false;
		PLYR.stats.ammo -= costAmmo;

		for (int i = 0; i < 10; i++)
		{
			GameObject thisBullet = Instantiate(bullet);
			thisBullet.transform.position = PLYR.item.useOrigin.position;
			thisBullet.transform.forward = Quaternion.AngleAxis(Random.Range(-10, 10f), PLYR.item.useOrigin.up) * Quaternion.AngleAxis(Random.Range(-10, 10f), PLYR.item.useOrigin.right) * PLYR.item.useOrigin.forward;
			if (thisBullet.GetComponent<Rigidbody>() != null)
			{
				thisBullet.GetComponent<Rigidbody>().AddForce(speed * thisBullet.transform.forward, ForceMode.VelocityChange);
			}
		}
		
		return true;
	}
}
