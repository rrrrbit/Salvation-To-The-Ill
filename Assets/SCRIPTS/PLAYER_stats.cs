using UnityEngine;

public class PLAYER_stats : Health
{
	public float ammo;
	public float maxAmmo;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
		ammo = Mathf.Clamp(ammo, 0, maxAmmo);
    }

	public override void Attack(AttackContext ctx)
	{
		base.Attack(ctx);
	}
}
