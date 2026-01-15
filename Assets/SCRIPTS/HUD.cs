using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public GameObject health;
	public GameObject ammo;
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

        for(int i = 0; i < PLYR.player.item.inventory.Length; i++)
        {
            if(PLYR.player.item.inventory[i])
            {
                if (PLYR.player.item.inventory[i].itemSprite)
                {
                    itemSprites[i].sprite = PLYR.player.item.inventory[i].itemSprite;
                }
                else
                {
                    itemSprites[i].sprite = placeholderSprite;
                }
            }
        }
        itemSelect.transform.position = GLOBAL.Lerpd(itemSelect.transform.position, itemSprites[PLYR.player.item.CurrentItem].transform.position, 0.75f, 0.02f, Time.deltaTime);
	}
}
