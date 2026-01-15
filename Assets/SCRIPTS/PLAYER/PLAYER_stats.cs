using UnityEngine;

public class PLAYER_stats : Stats
{
    public override void Convert(AttackContext ctx)
    {
        Destroy(gameObject);
    }
}
