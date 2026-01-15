using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class OBJ_Projectile : MonoBehaviour
{
	public LayerMask collideWith;
	public int originLayer;
	public WeaponStats originStats;
	public int originQuality;

	public AttackStats stats;
	public float lifetime;
	public int penetrationLeft;
	public AttackGroup group;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		penetrationLeft = originStats.penetration[originQuality];
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
		if (lifetime <= 0)
		{
			Destroy(gameObject);
		}
    }

	private void OnDestroy()
	{
        foreach (var obj in GetComponentsInChildren<Transform>())
        {
            obj.SetParent(transform.parent);
        }
    }

	private void OnTriggerEnter(Collider other)
	{

		if (collideWith.Contains(other.gameObject) && other.gameObject.layer != originLayer)
		{
			if(other.TryGetComponent(out IAttackable a))
			{
                AttackContext ctx = new()
                {
                    attackGroup = group,
                    target = other.gameObject,
                    baseDmg = Random.Range(originStats.dmgRange[originQuality].x, originStats.dmgRange[originQuality].y)
                };
                a.Attack(ctx);
				if(!other.TryGetComponent<PLYR>(out _)) MGR.vfx.DmgText(ctx, transform.position, false);
			}
			else
			{

			}
			penetrationLeft--; ;
			if(penetrationLeft == 0) Destroy(gameObject);
		}
	}
}
