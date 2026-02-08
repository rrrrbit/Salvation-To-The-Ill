using UnityEngine;
using UnityEngine.SceneManagement;

public class PLAYER_stats : Stats
{
	public AnimationCurve shakeDamageScreen;
	public AnimationCurve shakeDamageBar;
	public override void Convert(AttackContext ctx)
    {
		SceneManager.LoadScene(0);
    }

	public override void Die(AttackContext ctx)
	{
		SceneManager.LoadScene(0);
	}

	public override void Attack(AttackContext ctx)
	{
		base.Attack(ctx);
		entity.look.shake += Vector2.one * shakeDamageScreen.Evaluate(ctx.finalDmg / maxHealth);
		if (!ctx.heal)
		{
			HUD.hud.Overlay(HUD.OverlayType.damage);
			HUD.hud.healthShake += shakeDamageBar.Evaluate(ctx.baseDmg / maxHealth);
		}
		else if (ctx.attackerTeam == entity.team)
		{
			HUD.hud.Overlay(HUD.OverlayType.heal);
		}
		else
		{
			HUD.hud.Overlay(HUD.OverlayType.infect);
			HUD.hud.healthShake += shakeDamageBar.Evaluate(ctx.finalConv / maxHealth);
		}
	}
}
