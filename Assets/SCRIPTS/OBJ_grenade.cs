using UnityEngine;

public class OBJ_Grenade : OBJ_Projectile
{
    //public GameObject explosion;
    public float gravityMult;
    public float explosionSize;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * (gravityMult - 1), ForceMode.Force);
    }

    public override void OnDie()
    {
        foreach(Collider other in Physics.OverlapSphere(transform.position, explosionSize, collideWith))
        {
            if (other.TryGetComponent(out IAttackable a) && ((other.GetComponent<ENTITY>().team != originTeam) || originStats.heal) && other.gameObject != gameObject)
            {
                AttackContext ctx = new()
                {
                    attackGroup = group,
                    target = other.gameObject,
                    baseDmg = Random.Range(originStats.dmgRange[originQuality].x, originStats.dmgRange[originQuality].y),
                    heal = originStats.heal,
                    attackerTeam = originTeam,
                };
                a.Attack(ctx);
                if (!other.TryGetComponent<PLYR>(out _)) MGR.vfx.DmgText(ctx, other.transform.position, false);
            }
        }
    }
}
