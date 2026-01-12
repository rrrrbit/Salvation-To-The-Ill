using System.Diagnostics.Contracts;
using UnityEngine;

public class PLYR : ENTITY
{
	public PLAYER_cam cam;
    public static PLYR player;
    public override void Start()
    {
		base.Start();
        player = this;
		cam = GetComponent<PLAYER_cam>();
    }
}
