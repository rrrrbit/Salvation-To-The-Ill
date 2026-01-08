using UnityEngine;

public interface IAttackable
{
	void Attack(AttackContext ctx);
}

public class AttackContext
{
	public GameObject attacker;
	public GameObject target;
	public float baseDmg;
	public float finalDmg;
}
