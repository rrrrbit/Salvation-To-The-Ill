using System.Diagnostics.Contracts;
using UnityEngine;

public class PLYR : ENTITY
{
	public static PLAYER_cam cam;
    public override void Start()
    {
		base.Start();
		cam = GetComponent<PLAYER_cam>();
    }
}
