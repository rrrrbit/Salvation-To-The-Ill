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
				
				ShootRifle();
				shootTimer = shootInterval;
			}
		}
    }

	void ShootRifle()//temp, item switching is fully developed at home but i forgor to push >:(
	{
		GameObject thisBullet = Instantiate(bullet);
		thisBullet.transform.position = origin.position;
		thisBullet.transform.forward = origin.forward;
		if(thisBullet.GetComponent<Rigidbody>() != null)
		{
			thisBullet.GetComponent<Rigidbody>().AddForce(speed * origin.forward, ForceMode.VelocityChange);
		}
	}

	void ShootShotgun()
	{
		for (int i = 0; i < 10; i++)
		{
            GameObject thisBullet = Instantiate(bullet);
            thisBullet.transform.position = origin.position;
            thisBullet.transform.forward = Quaternion.AngleAxis(Random.Range(-10,10f),origin.up) * Quaternion.AngleAxis(Random.Range(-10, 10f), origin.right) * origin.forward;
            if (thisBullet.GetComponent<Rigidbody>() != null)
            {
                thisBullet.GetComponent<Rigidbody>().AddForce(speed * thisBullet.transform.forward, ForceMode.VelocityChange);
            }
        }
	}
}
