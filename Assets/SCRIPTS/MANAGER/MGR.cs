using UnityEngine;

public class MGR : MonoBehaviour
{
	public static MGR_vfx vfx;
	public static MGR_entity entities;
	public static MGR_game game;
	private void Start()
	{
		vfx = GetComponent<MGR_vfx>();
		entities = GetComponent<MGR_entity>();
		game = GetComponent<MGR_game>();
	}
}
