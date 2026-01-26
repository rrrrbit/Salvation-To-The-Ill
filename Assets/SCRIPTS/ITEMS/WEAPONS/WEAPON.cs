using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class WEAPON : UseBehaviour
{
    public WeaponStats stats;

    public virtual bool Consume(ENTITY entity)
    {
        var shootInterval = stats.shootInterval[Quality()];
        var costAmmo = stats.costAmmo[Quality()];
        var costHealth = stats.costHealth[Quality()];

        entity.inventory.cooldowns[entity.inventory.CurrentItem] = shootInterval;
        if (entity.stats.ammo < costAmmo || entity.stats.health < costHealth) return false;
        entity.stats.ammo -= costAmmo; entity.stats.health -= costHealth;
        return true;
    }
}
