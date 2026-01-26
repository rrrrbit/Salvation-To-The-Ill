using UnityEngine;

public class ITEM_restore : UseBehaviour
{
    public float healthRes;
    public float ammoRes;
    public float cooldown;
    public override bool TryUse(ENTITY user, ENTITY recipient)
    {
        recipient.stats.health += healthRes;
        recipient.stats.ammo += ammoRes;
        user.inventory.cooldowns[user.inventory.CurrentItem] = cooldown;
        item.amt--;
        return true;

    }
}
