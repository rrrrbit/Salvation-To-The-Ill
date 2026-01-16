using UnityEngine;

public class PLAYER_movement : Movement
{
    InpActions.PlayerActions actions;

    public override void Start()
    {
        var inp = new InpActions();
        inp.Enable();
        actions = inp.Player;
        actions.Enable();
		base.Start();
    }
    public override void FixedUpdate()
    {
        mvtIn = actions.move.ReadValue<Vector2>();
		jump = actions.Jump.IsPressed();

        base.FixedUpdate();
    }
}
