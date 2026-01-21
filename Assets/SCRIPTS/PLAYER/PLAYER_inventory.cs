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

        if (Physics.Raycast(new Ray(entity.look.cam.position, entity.look.cam.forward), out var hit, 2.5f, MGR.entities.entityLayers) && drop)
        {
            if (GetCurrent() && hit.collider.GetComponent<Inventory>().GetNextEmptySlot() != -1)
            {
                if(GetCurrent().TryGetComponent(out WEAPON i))
                {
                    hit.collider.GetComponent<Inventory>().TryPickUp(Drop(CurrentItem, -1, true));
                }
            }
        }

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
