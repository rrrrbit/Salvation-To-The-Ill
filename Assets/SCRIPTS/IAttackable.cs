using UnityEngine;

public interface IAttackable
{
	void Attack(AttackContext ctx);
}

[CreateAssetMenu(fileName = "AttackStats", menuName = "Game/Attack Stats", order = 1)]
public class AttackStats : ScriptableObject
{
	public float dmg;
}

public class AttackContext
{
	public GameObject gameObject;
	public float dmg;
}
