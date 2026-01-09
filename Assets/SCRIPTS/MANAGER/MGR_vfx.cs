using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MGR_vfx : MonoBehaviour
{
	public GameObject dmgText;
	public List<VFX_dmgText> dmgTexts;

	public void DmgText(AttackContext ctx, Vector3 position, bool flash)
	{
		var t = dmgTexts.Where(x =>
			x.ctx.target == ctx.target &&
			x.ctx.attackGroup.Equals(ctx.attackGroup));

        if (t.Count() > 0)
		{
			t.ToList()[0].value += ctx.finalDmg;
			t.ToList()[0].fadeTimer = t.ToList()[0].fadeTime;

            return;
		}
		
		var thisText = Instantiate(dmgText);
		thisText.transform.position = position;
		thisText.GetComponent<VFX_dmgText>().flashing = flash;
        thisText.GetComponent<VFX_dmgText>().ctx = ctx;
        thisText.GetComponent<VFX_dmgText>().value = ctx.finalDmg;

        Vector3 d = new Vector3(Random.Range(-1f, 1), Random.Range(0f, 1), Random.Range(-1f, 1)).normalized
					* Random.Range(.5f, 1) * 10;
		d.Scale(new(1, 2, 1));

		thisText.GetComponent<Rigidbody>().AddForce(d, ForceMode.VelocityChange);
		dmgTexts.Add(thisText.GetComponent<VFX_dmgText>());
	}
}
