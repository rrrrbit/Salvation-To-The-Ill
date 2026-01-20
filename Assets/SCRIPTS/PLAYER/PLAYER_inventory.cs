using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class PLAYER_inventory : Inventory
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
        interact = actions.interact.WasPressedThisFrame();
        drop = actions.drop.WasPressedThisFrame();
		base.Update();


        if (actions.scrollForward.WasPerformedThisFrame())
		{
            if(actions.swapItem.IsPressed())
            {
				inventory.TrySwap(CurrentItem, (CurrentItem - 1 + inventory.Length) % inventory.Length, out _);
			}
			CurrentItem--;
        }
		if (actions.scrollBackward.WasPerformedThisFrame())
		{
            if (actions.swapItem.IsPressed())
            {
                inventory.TrySwap(CurrentItem, (CurrentItem + 1 + inventory.Length) % inventory.Length, out _);
            }
            CurrentItem++;
        }

    }
}
