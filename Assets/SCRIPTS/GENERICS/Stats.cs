using UnityEngine;

public class Stats : MonoBehaviour, IAttackable
{
	public float health;
	public float maxHealth;
    public float ammo;
    public float maxAmmo;
    
	public virtual void Start()
	{
		ammo = maxAmmo;
		health = maxHealth;
	}

	public virtual void Update()
	{
        ammo = Mathf.Clamp(ammo, 0, maxAmmo);
        health = Mathf.Clamp(health, 0, maxHealth);
    }
	
	public virtual void Attack(AttackContext ctx)
	{
		print("i really dont appreciate you dealing ("+ctx.baseDmg.ToString()+") damage to me");
		health -= ctx.baseDmg;
		ctx.finalDmg = ctx.baseDmg;

		if(health <= 0)
		{
			Die();
		}
	}

	public virtual void Die()
	{
		Destroy(gameObject);
	}
}
