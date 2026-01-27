using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public GameObject health;
	public GameObject ammo;
    public GameObject convert;
    public TextMeshProUGUI infoText;
    public Image[] itemSprites;
    public Transform itemSelect;
    public Sprite placeholderSprite;
	public Color[] qualityColours;
	public static HUD hud;
    public Image overlay;
	public Sprite[] overlays;

	public float healthShake;
	Vector3 initHealthPos;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		hud = this;
		initHealthPos = health.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		health.GetComponent<Slider>().value = PLYR.player.stats.health / PLYR.player.stats.maxHealth;
		ammo.GetComponent<Slider>().value = PLYR.player.stats.ammo / PLYR.player.stats.maxAmmo;
        convert.GetComponent<Slider>().value = PLYR.player.stats.conversion / PLYR.player.stats.maxConversion;

		healthShake = GLOBAL.Lerpd(healthShake, 0, 0.5f, 0.1f, Time.deltaTime);

		var p = health.transform.position;
		p.x = initHealthPos.x + Random.Range(-1, 1) * healthShake;
		health.transform.position = p;

		var c = overlay.color;
		c.a = Mathf.Max(0, c.a - Time.deltaTime);
		overlay.color = c;

		for (int i = 0; i < PLYR.player.inventory.inventory.Length; i++)
        {
            itemSprites[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            if(PLYR.player.inventory.inventory[i])
            {
                var thisItem = PLYR.player.inventory.inventory[i];

                if (thisItem.itemSprite) itemSprites[i].sprite = thisItem.itemSprite;
                else itemSprites[i].sprite = placeholderSprite;

				itemSprites[i].color = qualityColours[(int)thisItem.quality];
                if (thisItem.amt > 1)
                {
                    itemSprites[i].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                    itemSprites[i].GetComponentInChildren<TextMeshProUGUI>().text = thisItem.amt.ToString();
                }
            }
            else
            {
                itemSprites[i].sprite = null;
				itemSprites[i].color = Color.white;
			}
        }

        itemSelect.transform.position = GLOBAL.Lerpd(itemSelect.transform.position, itemSprites[PLYR.player.inventory.CurrentItem].transform.position, 0.75f, 0.02f, Time.deltaTime);

		if (MGR.game.grace)
		{
			infoText.text = "grace period" +
				"\n" + (Mathf.Round(MGR.game.graceTimer * 100f) / 100f).ToString();
		}
		else if(MGR.entities.CountTeam(ENTITY.Teams.ZOMBIE) <= 0)
		{
			infoText.text = "wave cleared" +
				"\n" + (Mathf.Round(MGR.game.waveTimer * 100f) / 100f).ToString();
		}
		else
		{
			infoText.text = "wave " + MGR.game.wave.ToString() +
				"\n" + (Mathf.Round(MGR.game.waveTimer * 100f) / 100f).ToString() +
				"\n" + MGR.entities.CountTeam(ENTITY.Teams.ZOMBIE) + " zombies left";
		}
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
