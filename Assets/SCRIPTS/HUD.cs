using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	#region declarations
	public static HUD hud;
    public Image overlay;
	public Sprite[] overlays;
	[Header("")]
	public GameObject health;
	public float healthShake;
	Vector3 initHealthPos;
	[Header("")]
	public GameObject ammo;
    public GameObject convert;
	public TextMeshProUGUI healthText;
	public TextMeshProUGUI ammoText;
	[Header("")]
	public Image[] itemSprites;
	public Image[] itemSpritesParents;
	public Image[] itemSpritesBgs;
	public Transform itemSelect;
    public Sprite placeholderSprite;
	public Color[] qualityColours;
	[Header("")]
	public TextMeshProUGUI waveWidgetTitle;
	public TextMeshProUGUI waveWidgetBody;
	public UI_gradient waveWidgetGradient;
	public Gradient healDmgColors;
	public Gradient healGradient;
	public Gradient dmgGradient;
	public float currentWaveWidgetColor;
	public float waveWidgetColourTransitionTime;
	public bool waveWidgetGreen;
	[Header("")]
	public TextMeshProUGUI killText;
	public TextMeshProUGUI healText;
	#endregion
    void Start()
    {
		hud = this;
		initHealthPos = health.transform.position;
    }

    void Update()
    {
		var c = overlay.color;
		c.a = Mathf.Max(0, c.a - Time.deltaTime);
		overlay.color = c;

		UpdateBars();
		UpdateInventorySlots();
		UpdateTexts();
	}

	void UpdateBars()
	{
		health.GetComponentInChildren<Slider>().value = PLYR.player.stats.health / PLYR.player.stats.maxHealth;
		ammo.GetComponent<Slider>().value = PLYR.player.stats.ammo / PLYR.player.stats.maxAmmo;
		convert.GetComponent<Slider>().value = PLYR.player.stats.conversion / PLYR.player.stats.maxConversion;

		healthShake = GLOBAL.Lerpd(healthShake, 0, 0.5f, 0.1f, Time.deltaTime);

		var p = health.transform.position;
		p.x = initHealthPos.x + Random.Range(-1, 1) * healthShake;
		health.transform.position = p;
	}
	void UpdateInventorySlots()
	{
		for (int i = 0; i < PLYR.player.inventory.inventory.Length; i++)
		{
			itemSpritesParents[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
			if (PLYR.player.inventory.inventory[i])
			{
				var thisItem = PLYR.player.inventory.inventory[i];

				if (thisItem.itemSprite) itemSprites[i].sprite = thisItem.itemSprite;
				else itemSprites[i].sprite = placeholderSprite;

				itemSpritesBgs[i].color = qualityColours[(int)thisItem.quality];
				if (thisItem.amt > 1)
				{
					itemSpritesParents[i].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
					itemSpritesParents[i].GetComponentInChildren<TextMeshProUGUI>().text = thisItem.amt.ToString();
				}
			}
			else
			{
				itemSprites[i].sprite = null;
				itemSpritesBgs[i].color = Color.white;
			}
		}

		itemSelect.transform.position = Mathv.Lerpd(itemSelect.transform.position, itemSprites[PLYR.player.inventory.CurrentItem].transform.position, 0.75f, 0.02f, Time.deltaTime);
	}
	void UpdateTexts()
	{
		waveWidgetTitle.color = healDmgColors.Evaluate(currentWaveWidgetColor);
		waveWidgetBody.color = healDmgColors.Evaluate(currentWaveWidgetColor);
		waveWidgetGradient.gradient = GLOBAL.Lerp(dmgGradient, healGradient, currentWaveWidgetColor);

		currentWaveWidgetColor += Time.deltaTime / waveWidgetColourTransitionTime * (waveWidgetGreen ? 1 : -1);
		currentWaveWidgetColor = Mathf.Clamp01(currentWaveWidgetColor);

		if (MGR.game.grace)
		{
			waveWidgetTitle.text = "GRACE PERIOD";
			waveWidgetBody.text = (Mathf.Round(MGR.game.graceTimer * 100f) / 100f).ToString();
			waveWidgetGreen = true;
		}
		else if (MGR.entities.CountTeam(ENTITY.Teams.ZOMBIE) <= 0)
		{
			waveWidgetTitle.text = "WAVE CLEARED";
			waveWidgetBody.text = (Mathf.Round(MGR.game.waveTimer * 100f) / 100f).ToString();
			waveWidgetGreen = false;
		}
		else
		{
			waveWidgetTitle.text = "WAVE " + MGR.game.wave.ToString();
			waveWidgetBody.text = (Mathf.Round(MGR.game.waveTimer * 100f) / 100f).ToString() +
				"\n" + MGR.entities.CountTeam(ENTITY.Teams.ZOMBIE) + " LEFT";
			waveWidgetGreen = false;
		}

		killText.text = PLYR.player.kills.ToString();
		healText.text = MGR.entities.CountTeam(ENTITY.Teams.HUMAN).ToString();

		healthText.text = Mathf.Round(PLYR.player.stats.health).ToString();
		ammoText.text = Mathf.Round(PLYR.player.stats.ammo).ToString();
	}

	public enum OverlayType
	{
		damage = 0,
		heal = 1,
		infect = 2,
	}

    public void Overlay(OverlayType overlayType)
    {
		var c = overlay.color;
		c.a = 0.5f;
		overlay.color = c;
		overlay.sprite = overlays[(int)overlayType];
    }
}
