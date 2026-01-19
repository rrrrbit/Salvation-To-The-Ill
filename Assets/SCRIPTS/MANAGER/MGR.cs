using UnityEngine;

public class MGR : MonoBehaviour
{
	public static MGR_vfx vfx;
	public static MGR_entity entities;
	public static Transform itemParents;
	private void Start()
	{
		vfx = GetComponent<MGR_vfx>();
		entities = GetComponent<MGR_entity>();
	}
}
