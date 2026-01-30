using UnityEngine;

public class OBJ_bullet : OBJ_Projectile
{
    public AudioClip hitSound;
    public override void OnHit(Collider other, bool instakill)
    {
		MGR.vfx.BulletPtcl(originStats.heal, other.ClosestPoint(transform.position-GetComponent<Rigidbody>().linearVelocity*Time.fixedDeltaTime), GetComponent<Rigidbody>().linearVelocity);
		if (instakill)
		{
			Destroy(gameObject);
			return;
		}
		
		
		if (other.TryGetComponent(out IAttackable a))
        {
            AttackContext ctx = new()
            {
                attackGroup = group,
                target = other.gameObject,
                baseDmg = Random.Range(originStats.dmgRange[originQuality].x, originStats.dmgRange[originQuality].y),
                baseConv = Random.Range(originStats.conversionRange[originQuality].x, originStats.conversionRange[originQuality].y),
                heal = originStats.heal,
                attackerTeam = origin.GetComponent<ENTITY>().team,
				fromPlayer = fromPlayer
            };
            a.Attack(ctx);
            if (!other.TryGetComponent<PLYR>(out _)) MGR.vfx.DmgText(ctx, transform.position, false);
        }
        else
        {

        }
        penetrationLeft--;
        MGR.audio.PlaySoundOmnipresent(hitSound);
        if (penetrationLeft == 0) Destroy(gameObject);
    }
}
