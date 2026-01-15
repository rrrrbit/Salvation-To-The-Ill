using UnityEngine;

public class NPC_stats : Stats
{
    public override void Convert(AttackContext ctx)
    {
        if (entity.team == ENTITY.Teams.HUMAN) entity.team = ENTITY.Teams.ZOMBIE;
        else entity.team = ENTITY.Teams.HUMAN;

        conversion = 0;
    }
}
