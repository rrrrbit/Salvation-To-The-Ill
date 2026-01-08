using UnityEngine;

public class Health : MonoBehaviour, IAttackable
{
	public float health;
	public float maxHealth;
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
