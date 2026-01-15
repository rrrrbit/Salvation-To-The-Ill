using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class OBJ_Projectile : MonoBehaviour
{
	public LayerMask collideWith;
	public LayerMask instakill;
    public float lifetime;
    public int penetrationLeft;
    public AttackGroup group;
	public bool targetOriginLayer;

    public int originLayer;
	public WeaponStats originStats;
	public int originQuality;

    void Start()
    {
		penetrationLeft = originStats.penetration[originQuality];
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
		if (lifetime <= 0)
		{
			OnDie();
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
		if (instakill.Contains(other.gameObject))
		{
			OnDie();
			Destroy(gameObject);
		}
		else if (collideWith.Contains(other.gameObject) && other.gameObject.layer != originLayer || targetOriginLayer) OnHit(other);
	}

    public virtual void OnHit(Collider other)
	{

	}

	public virtual void OnDie()
	{

	}
}
