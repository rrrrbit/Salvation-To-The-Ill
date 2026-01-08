using UnityEngine;

public class PLYR : MonoBehaviour
{
	public static GameObject obj;
	public static Transform t;
	public static PLAYER_cam cam;
	public static PLAYER_stats stats;
	public static PLAYER_shoot shoot;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		obj = gameObject;
		t = transform;
		cam = GetComponent<PLAYER_cam>();
		stats = GetComponent<PLAYER_stats>();
		shoot = GetComponent<PLAYER_shoot>();
    }
}
