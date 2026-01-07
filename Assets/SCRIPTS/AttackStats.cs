using UnityEngine;

[CreateAssetMenu(fileName = "AttackStats", menuName = "Game/Attack Stats", order = 1)]
public class AttackStats : ScriptableObject
{
    public float dmg;
    public int penetration;
}
