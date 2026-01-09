using UnityEngine;

public interface IAttackable
{
	void Attack(AttackContext ctx);
}

public class AttackContext
{
	public AttackGroup attackGroup;
	public GameObject target;
	public float baseDmg;
	public float finalDmg;
}

public class AttackGroup { }
