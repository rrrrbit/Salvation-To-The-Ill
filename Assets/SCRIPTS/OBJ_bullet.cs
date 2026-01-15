using UnityEngine;

public class OBJ_bullet : OBJ_Projectile
{
    public override void OnHit(Collider other)
    {
        if (other.TryGetComponent(out IAttackable a))
        {
            AttackContext ctx = new()
            {
                attackGroup = group,
                target = other.gameObject,
                baseDmg = Random.Range(originStats.dmgRange[originQuality].x, originStats.dmgRange[originQuality].y)
            };
            a.Attack(ctx);
            if (!other.TryGetComponent<PLYR>(out _)) MGR.vfx.DmgText(ctx, transform.position, false);
        }
        else
        {

        }
        penetrationLeft--;
        if (penetrationLeft == 0) Destroy(gameObject);
    }
}
