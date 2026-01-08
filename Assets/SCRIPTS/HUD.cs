using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public GameObject health;
	public GameObject ammo;
    public Image[] itemSprites;
    public Transform itemSelect;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		health.GetComponent<Slider>().value = PLYR.stats.health / PLYR.stats.maxHealth;
		ammo.GetComponent<Slider>().value = PLYR.stats.ammo / PLYR.stats.maxAmmo;

        for(int i = 0; i < PLYR.item.inventory.Length; i++)
        {
            if(PLYR.item.inventory[i] && PLYR.item.inventory[i].itemSprite)
            {
                itemSprites[i].sprite = PLYR.item.inventory[i].itemSprite;
            }
        }
        itemSelect.transform.position = itemSprites[PLYR.item.CurrentItem].transform.position;
	}
}
