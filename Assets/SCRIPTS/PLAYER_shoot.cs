using UnityEngine;

public class PLAYER_shoot : MonoBehaviour
{
	InpActions.PlayerActions actions;
	public GameObject bullet;
	public float speed;
	public Transform origin;
	public float costAmmo;
	public float costHealth;

	public float shootInterval;
	float shootTimer;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		var inp = new InpActions();
		inp.Enable();
		actions = inp.Player;
		actions.Enable();
	}

    // Update is called once per frame
    void Update()
    {
		shootTimer = Mathf.Max(0, shootTimer - Time.deltaTime);
		
		if (actions.shoot.IsPressed() && shootTimer <= 0)
		{
			if(PLYR.stats.ammo >= costAmmo)
			{
				PLYR.stats.ammo -= costAmmo;
				PLYR.stats.health -= costHealth;
				
				Shoot();
				shootTimer = shootInterval;
			}
		}
    }

	void Shoot()
	{
		GameObject thisBullet = Instantiate(bullet);
		thisBullet.transform.position = origin.position;
		thisBullet.transform.forward = origin.forward;
		if(thisBullet.GetComponent<Rigidbody>() != null)
		{
			thisBullet.GetComponent<Rigidbody>().AddForce(speed * origin.forward, ForceMode.VelocityChange);
		}
	}
}
