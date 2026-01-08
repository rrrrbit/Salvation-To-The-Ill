using UnityEngine;

[CreateAssetMenu(fileName = "AttackStats", menuName = "Game/Attack Stats", order = 1)]
public class AttackStats : ScriptableObject
{
	public float minDmg;
	public float maxDmg;
    public int penetration;
}
