using UnityEngine;

public class OBJ_Grenade : OBJ_Projectile
{
    public float explosionSize;
    public AnimationCurve shake;
    public GameObject rangeIndicator;
	public AnimationCurve flashSpeed;

	public float flashTimer;

    public AudioClip explosion;
    public override void OnDie()
    {
        Destroy(rangeIndicator);
        foreach(Collider other in Physics.OverlapSphere(transform.position, explosionSize, collideWith))
        {
            if (other.TryGetComponent(out IAttackable a) && other.gameObject != origin && (other.GetComponent<ENTITY>().team != originTeam || originStats.heal) && other.gameObject != gameObject)
            {
                AttackContext ctx = new()
                {
                    attackGroup = group,
                    target = other.gameObject,
                    baseDmg = Random.Range(originStats.dmgRange[originQuality].x, originStats.dmgRange[originQuality].y),
                    baseConv = Random.Range(originStats.conversionRange[originQuality].x, originStats.conversionRange[originQuality].y),
                    heal = originStats.heal,
                    attackerTeam = originTeam,
					fromPlayer = fromPlayer
                };
                a.Attack(ctx);
                if (!other.TryGetComponent<PLYR>(out _)) MGR.vfx.DmgText(ctx, other.transform.position, false);
            }
        }
        foreach (Collider other in Physics.OverlapSphere(transform.position, shake.keys[shake.keys.Length-1].time))
        {
            if (other.TryGetComponent(out Look look))
            {
                look.shake += Vector2.one * shake.Evaluate((transform.position - other.transform.position).magnitude);
            }
        }
        MGR.vfx.Explosion(originStats.heal, explosionSize, transform);
        MGR.audio.PlayAtPos(explosion, transform.position);
    }

    public override void Update()
    {
        base.Update();
		flashTimer += Time.deltaTime * flashSpeed.Evaluate(lifetime);
        Material mat = rangeIndicator.GetComponent<Renderer>().material;
        mat.color = GetComponent<Renderer>().material.color * (1 - flashTimer % 1);
        rangeIndicator.transform.localScale = Mathv.Lerpd(rangeIndicator.transform.localScale, new Vector3(explosionSize, explosionSize, explosionSize).DivideBy(transform.localScale) * 2, 0.2f, 0.05f, Time.deltaTime);
    }
}
