using UnityEngine;

public class ITEM_restore : UseBehaviour
{
    public float healthRes;
    public float ammoRes;
    public float cooldown;
    public override bool TryUse(ENTITY user, ENTITY recipient, bool asHand = false)
    {
        recipient.stats.health += healthRes;
        recipient.stats.ammo += ammoRes;

		if (asHand)
		{
			user.inventory.cooldowns[user.inventory.CurrentItem] = cooldown;

		}
		else
		{ 
			user.inventory.handCooldown = cooldown;
		}

        item.amt--;
        return true;

    }
}
