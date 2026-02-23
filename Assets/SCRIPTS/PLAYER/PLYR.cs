using System.Diagnostics.Contracts;
using UnityEngine;

public class PLYR : ENTITY
{
    public static PLYR player;
	public int kills;
	public int heals;
	InpActions.PlayerActions actions;
	public override void Start()
    {
		var inp = new InpActions();
		inp.Enable();
		actions = inp.Player;
		actions.Enable();
		base.Start();
        player = this;
    }

	public override void Update()
	{
		movement.mvtIn = transform.rotation * actions.move.ReadValue<Vector2>().xz(0);
		movement.jump = actions.Jump.IsPressed();
		base.Update();
	}
}
