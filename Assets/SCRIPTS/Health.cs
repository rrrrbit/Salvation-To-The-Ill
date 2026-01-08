using UnityEngine;

public class Health : MonoBehaviour, IAttackable
{
	public float health;
	public float maxHealth;
	public virtual void Attack(AttackContext ctx)
	{
		print("i really dont appreciate you dealing ("+ctx.dmg.ToString()+") damage to me");
	}

	public virtual void Die()
	{

	}
}
