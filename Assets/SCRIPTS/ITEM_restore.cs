using UnityEngine;

public class ITEM_restore : UseBehaviour
{
    public float healthRes;
    public float ammoRes;
    public float cooldown;
    public override bool TryUse(ENTITY entity)
    {
        entity.stats.health += healthRes;
        entity.stats.ammo += ammoRes;
        entity.inventory.useTimer = cooldown;
        item.amt--;
        return true;

    }
}
