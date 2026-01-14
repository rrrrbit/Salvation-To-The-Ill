using TMPro;
using UnityEditor;
using UnityEngine;

public class PLAYER_item : Item
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
    public override void Update()
    {
		use = actions.shoot.IsPressed();
		base.Update();


        if (actions.scrollForward.WasPerformedThisFrame()) CurrentItem--;
		if (actions.scrollBackward.WasPerformedThisFrame()) CurrentItem++;

    }
}
