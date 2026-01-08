using UnityEngine;

public class PLYR : MonoBehaviour
{
	public static GameObject obj;
	public static Transform t;
	public static PLAYER_cam cam;
	public static PLAYER_stats stats;
	public static PLAYER_item item;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		obj = gameObject;
		t = transform;
		cam = GetComponent<PLAYER_cam>();
		stats = GetComponent<PLAYER_stats>();
		item = GetComponent<PLAYER_item>();
    }
}
