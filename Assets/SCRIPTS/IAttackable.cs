using UnityEngine;

public interface IAttackable
{
	void Attack(AttackContext ctx);
}

public class AttackContext
{
	public GameObject gameObject;
	public float dmg;
}
