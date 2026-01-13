using UnityEngine;

public class MGR : MonoBehaviour
{
	public static MGR_vfx vfx;
	public static MGR_npc npc;
	private void Start()
	{
		vfx = GetComponent<MGR_vfx>();
		npc = GetComponent<MGR_npc>();
	}
}
