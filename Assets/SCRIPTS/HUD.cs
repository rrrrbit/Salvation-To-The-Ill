using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public GameObject health;
	public GameObject ammo;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		health.GetComponent<Slider>().value = PLYR.stats.health / PLYR.stats.maxHealth;
		ammo.GetComponent<Slider>().value = PLYR.stats.ammo / PLYR.stats.maxAmmo;
	}
}
