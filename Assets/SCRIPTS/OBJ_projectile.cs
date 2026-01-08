using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class OBJ_Projectile : MonoBehaviour
{
	public LayerMask collideWith;
	public AttackStats stats;
	public float lifetime;
	public int penetrationLeft;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		penetrationLeft = stats.penetration;
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
		
		if ((collideWith & (1 << other.gameObject.layer)) != 0)
		{
			if(other.gameObject.GetComponent<IAttackable>() != null)
			{
				AttackContext ctx = new();
				ctx.gameObject = gameObject;
				ctx.dmg = Random.Range(stats.minDmg, stats.maxDmg);
				other.gameObject.GetComponent<IAttackable>().Attack(ctx);
				

				MGR.vfx.DmgText(ctx.dmg, transform.position, false);
			}
			else
			{

			}
            penetrationLeft -= 1;
			if(penetrationLeft == 0) Destroy(gameObject);
		}
	}
}
