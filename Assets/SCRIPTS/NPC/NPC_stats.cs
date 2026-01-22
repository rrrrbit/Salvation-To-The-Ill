using UnityEngine;

public class NPC_stats : Stats
{
    public override void Convert(AttackContext ctx)
    {
        if (entity.team == ENTITY.Teams.HUMAN) entity.team = ENTITY.Teams.ZOMBIE;
        else entity.team = ENTITY.Teams.HUMAN;

        conversion = 0;
        convResistance += 0.25f;
    }

    public override void Update()
    {
        base.Update();
        ammo = maxAmmo;
    }

    public override void Die(AttackContext ctx)
    {
        for (int i = 0; i < entity.inventory.inventory.Length; i++)
        {
            entity.inventory.Drop(i, -1, false);
        }
            base.Die(ctx);
    }
}
