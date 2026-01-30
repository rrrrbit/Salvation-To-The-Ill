using System.Diagnostics.Contracts;
using UnityEngine;

public class PLYR : ENTITY
{
    public static PLYR player;
	public int kills;
	public int heals;
    public override void Start()
    {
		base.Start();
        player = this;
    }
}
