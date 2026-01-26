using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.EventSystems.EventTrigger;

public class OBJ_Projectile : MonoBehaviour
{
	public LayerMask collideWith;
	public LayerMask instakill;
    public float lifetime;
    public int penetrationLeft;
    public AttackGroup group;
	public bool targetOriginLayer;

	public GameObject origin;
	public WeaponStats originStats;
	public int originQuality;

	public ENTITY.Teams originTeam;

    void Start()
    {
		penetrationLeft = originStats.penetration[originQuality];
		originTeam = origin.GetComponent<ENTITY>().team;
    }

    public virtual void Update()
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
		else if (collideWith.Contains(other.gameObject) && ((other.GetComponent<ENTITY>().team != originTeam) || originStats.heal) && other.gameObject != origin) OnHit(other);
	}

    public virtual void OnHit(Collider other)
	{

	}

	public virtual void OnDie()
	{

	}
}
