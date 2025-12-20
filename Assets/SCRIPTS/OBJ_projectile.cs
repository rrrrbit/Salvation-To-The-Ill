using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class OBJ_Projectile : MonoBehaviour
{
	public LayerMask collideWith;
	public AttackStats stats;
	public float lifetime;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
		transform.GetChild(0).SetParent(transform.parent);
	}

	private void OnTriggerEnter(Collider other)
	{
		
		if ((collideWith & (1 << other.gameObject.layer)) != 0)
		{
			if(other.gameObject.GetComponent<IAttackable>() != null)
			{
				AttackContext ctx = new();
				ctx.gameObject = gameObject;
				ctx.dmg = stats.dmg;
				other.gameObject.GetComponent<IAttackable>().Attack(ctx);

				MGR.vfx.DmgText(stats.dmg, transform.position, false);
			}
			else
			{

			}
			Destroy(gameObject);
		}
	}
}
