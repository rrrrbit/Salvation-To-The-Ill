using UnityEngine;

public class Stats : MonoBehaviour, IAttackable
{
	public float health;
    public float ammo;
	public float conversion;
	[Space]
	public float maxHealth;
    public float maxAmmo;
	public float maxConversion;
	public float speed;
	public float size;
	public float defense;
	public float convResistance;
	public float attackSpeedMult;
	public float ammoCnsmpMult;
	[Space]
    public ENTITY entity;
    public virtual void Start()
	{
		ammo = maxAmmo;
		health = maxHealth;
	}

	public virtual void Update()
	{
        ammo = Mathf.Clamp(ammo, 0, maxAmmo);
        health = Mathf.Clamp(health, 0, maxHealth);
		conversion = Mathf.Clamp(conversion, 0, maxConversion);
    }
	
	public virtual void Attack(AttackContext ctx)
	{
		if (ctx.attackerTeam == entity.team) ctx.finalDmg = 0;
        else ctx.finalDmg = ctx.baseDmg * DefenseMult(defense);
        health -= ctx.finalDmg;

        ctx.finalConv = ctx.baseConv * DefenseMult(convResistance);
		print(ctx.finalConv);
        if (ctx.attackerTeam == entity.team)
        {
            Heal(ctx.baseConv);
            conversion -= ctx.finalConv;
        }
        else conversion += ctx.finalConv;

		if(health <= 0) Die(ctx);
        if (conversion >= maxConversion) Convert(ctx);
    }

	public virtual void Heal(float amt)
	{
		health += amt;
	}

	public virtual void Convert(AttackContext ctx)
	{

	}

	public virtual void Die(AttackContext ctx)
	{
		Destroy(gameObject);
	}

	float DefenseMult(float x)
	{
		if (x <= 0) return 1 - x; else return 1 / (x+1);
	}
}
