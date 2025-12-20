using UnityEngine;

public class Health : MonoBehaviour, IAttackable
{
	public float health;
	public float maxHealth;
	public virtual void Attack(AttackContext ctx)
	{
		print("OW FUCKING DICKHEAD ("+ctx.dmg.ToString()+")");
	}

	public virtual void Die()
	{

	}
}
