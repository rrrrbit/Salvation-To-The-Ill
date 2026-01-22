using UnityEngine;

public class OBJ_Grenade : OBJ_Projectile
{
    public float explosionSize;

    public override void OnDie()
    {
        foreach(Collider other in Physics.OverlapSphere(transform.position, explosionSize, collideWith))
        {
            if (other.TryGetComponent(out IAttackable a) && ((other.gameObject != origin) || originStats.heal) && other.gameObject != gameObject)
            {
                AttackContext ctx = new()
                {
                    attackGroup = group,
                    target = other.gameObject,
                    baseDmg = Random.Range(originStats.dmgRange[originQuality].x, originStats.dmgRange[originQuality].y),
                    baseConv = Random.Range(originStats.conversionRange[originQuality].x, originStats.conversionRange[originQuality].y),
                    heal = originStats.heal,
                    attackerTeam = originTeam,
                };
                a.Attack(ctx);
                if (!other.TryGetComponent<PLYR>(out _)) MGR.vfx.DmgText(ctx, other.transform.position, false);
            }
        }
    }
}
