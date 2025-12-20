using UnityEngine;

public class MGR : MonoBehaviour
{
	public static MGR_vfx vfx;
	private void Start()
	{
		vfx = GetComponent<MGR_vfx>();
	}
}
