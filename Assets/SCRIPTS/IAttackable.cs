using UnityEngine;

public interface IAttackable
{
	void Attack(AttackContext ctx);
	void AddConversion(AttackContext ctx);
}

public class AttackContext
{
	public AttackGroup attackGroup;
	public ENTITY.Teams attackerTeam;
	public ENTITY.Teams targetTeam;
	public GameObject target;
	public float baseDmg;
	public float finalDmg;
	public float baseConv;
	public float finalConv;
}

public class AttackGroup { }
