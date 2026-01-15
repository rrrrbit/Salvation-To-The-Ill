using UnityEngine;
using static UnityEngine.UI.Image;

public class WEAPON_singleShot : WEAPON
{
	public GameObject bullet;
    public Vector2 spread;
    public float speed;

	public override bool TryUse(ENTITY entity)
	{
		if(!Consume(entity)) return false;

		GameObject thisBullet = Instantiate(bullet);
		thisBullet.transform.position = entity.item.useOrigin.position;
        Vector2 angle = Random.insideUnitCircle.Scaled(new(spread.x / 2, spread.y / 2));


        thisBullet.transform.forward = Quaternion.AngleAxis(angle.x, entity.item.useOrigin.up) * 
									   Quaternion.AngleAxis(angle.y, entity.item.useOrigin.right) * 
									   entity.item.useOrigin.forward;

		if (thisBullet.TryGetComponent(out Rigidbody r))
		{
			r.AddForce(speed * thisBullet.transform.forward, ForceMode.VelocityChange);
		}
        if(thisBullet.TryGetComponent(out OBJ_Projectile p))
		{ 
			p.group = new(); 
			p.originLayer = entity.obj.layer;
		}
        return true;
	}
}
