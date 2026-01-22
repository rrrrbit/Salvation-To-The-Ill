using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public GameObject health;
	public GameObject ammo;
    public GameObject convert;
    public Image[] itemSprites;
    public Transform itemSelect;
    public Sprite placeholderSprite;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		health.GetComponent<Slider>().value = PLYR.player.stats.health / PLYR.player.stats.maxHealth;
		ammo.GetComponent<Slider>().value = PLYR.player.stats.ammo / PLYR.player.stats.maxAmmo;
        convert.GetComponent<Slider>().value = PLYR.player.stats.conversion / PLYR.player.stats.maxConversion;

        for(int i = 0; i < PLYR.player.inventory.inventory.Length; i++)
        {
            itemSprites[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            if(PLYR.player.inventory.inventory[i])
            {
                var thisItem = PLYR.player.inventory.inventory[i];
                if (thisItem.itemSprite)
                {
                    itemSprites[i].sprite = thisItem.itemSprite;
                }
                else
                {
                    itemSprites[i].sprite = placeholderSprite;
                }
                if (thisItem.amt > 1)
                {
                    itemSprites[i].GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                    itemSprites[i].GetComponentInChildren<TextMeshProUGUI>().text = thisItem.amt.ToString();
                }
            }
            else
            {
                itemSprites[i].sprite = null;
            }
        }
        itemSelect.transform.position = GLOBAL.Lerpd(itemSelect.transform.position, itemSprites[PLYR.player.inventory.CurrentItem].transform.position, 0.75f, 0.02f, Time.deltaTime);
	}
}
