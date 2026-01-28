using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MGR_vfx : MonoBehaviour
{
	public GameObject dmgText;
	public GameObject healText;
	public GameObject explosion;
    public GameObject healExplosion;
	public GameObject hitPtcl;
	public GameObject hitPtclHeal;

	[Space]
    public List<VFX_dmgText> dmgTexts;

    public void DmgText(AttackContext ctx, Vector3 position, bool flash)
	{
		if ((ctx.heal ? ctx.finalConv : ctx.finalDmg) <= 0) return;

		IEnumerable<VFX_dmgText> t = dmgTexts.Where(x =>
			x.ctx.attackGroup.Equals(ctx.attackGroup) &&
			x.ctx.target == ctx.target && 
			x.ctx.heal == ctx.heal);

        if (t.Count() > 0)
		{
			t.ToList()[0].value += ctx.heal ? ctx.finalConv : ctx.finalDmg;
			t.ToList()[0].fadeTimer = t.ToList()[0].fadeTime;

            return;
		}

		GameObject thisText = Instantiate(ctx.heal ? healText : dmgText, position, new());
		VFX_dmgText text = thisText.GetComponent<VFX_dmgText>();
		text.flashing = flash;
		text.ctx = ctx;
		text.value = ctx.heal ? ctx.finalConv : ctx.finalDmg;

        Vector3 d = new Vector3(Random.Range(-1f, 1), Random.Range(0f, 1), Random.Range(-1f, 1)).normalized.Scaled(new(1,2,1))
					* Random.Range(.5f, 1) * 10;

		thisText.GetComponent<Rigidbody>().AddForce(d, ForceMode.VelocityChange);
		dmgTexts.Add(text);
	}

	public void Explosion(bool heal, float range, Transform pos)
	{
		VFX_explosion expl = Instantiate(heal ? healExplosion : explosion, pos.position, new()).GetComponent<VFX_explosion>();
		expl.range = range*4;
		expl.speed = 20;
	}

	public void BulletPtcl(bool heal, Vector3 pos, Vector3 velocity)
	{
		Quaternion angle = Quaternion.LookRotation(velocity);
		GameObject thisPtcl = Instantiate(heal ? hitPtclHeal : hitPtcl, pos, angle);
		ParticleSystem.MainModule main = thisPtcl.GetComponent<ParticleSystem>().main;
		main.startSpeed = new(0, velocity.magnitude / 3);
	}
}
