using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public GameObject hand;
	public Transform useOrigin;
	public float shootTimer;
	public float useTimer;

	public int invSize;
	public ItemData[] inventory;
	public int CurrentItem
	{
		get { return currentItem; }
        set { currentItem = (value + inventory.Length) % inventory.Length; }
    }
	int currentItem;

	public bool use;
	public bool interact;
	public ENTITY entity;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	
    public virtual void Start()
    {
		inventory = new ItemData[invSize];
	}

    // Update is called once per frame
    public virtual void Update()
    {
		shootTimer = Mathf.Max(0, shootTimer - Time.deltaTime);
		useTimer = Mathf.Max(0, useTimer - Time.deltaTime);


		if (use && shootTimer <= 0 && useTimer <= 0)
		{
			UseBehaviour useBehaviour;
			if (inventory[CurrentItem] && inventory[CurrentItem].TryGetComponent(out useBehaviour))
			{
				useBehaviour.TryUse(entity);
			}
			else
			{
				hand.GetComponent<UseBehaviour>().TryUse(entity);
			}
		}


		for (int i = 0; i < inventory.Length; i++)
		{
			if (inventory[i])
			{
				if(inventory[i].amt <= 0)
				{
					Destroy(inventory[i].gameObject);
					inventory[i] = null;
				}
				if (inventory[i].amt > inventory[i].maxStack)
				{
					
					if(GetNextEmptySlot() == -1)
					{
						Drop(i, inventory[i].amt - inventory[i].maxStack, true);
					}
					else
					{
						var newStack = Instantiate(inventory[i].gameObject, MGR.itemParents);
						newStack.GetComponent<ItemData>().amt = inventory[i].amt - inventory[i].maxStack;
						inventory[i].amt = inventory[i].maxStack;
                    }
				}
			}
		}

		if(Physics.Raycast(new Ray(entity.look.cam.position, entity.look.cam.forward), out var hit, 2.5f, MGR.entities.pickupLayer) && interact)
		{
			TryPickUp(hit.collider.gameObject);
		}
    }
	public ItemData GetCurrent()
	{
        if (inventory[CurrentItem]) return inventory[CurrentItem];
		else return null;
	}

	public void Drop(int slot, int amount, bool directed)
	{
		inventory[slot].amt -= amount;
	}

	public bool TryPickUp(GameObject obj)
	{
		if(!obj.TryGetComponent(out OBJ_pickup pickup) || 
			!pickup.item.TryGetComponent(out ItemData item) ||
            GetNextEmptySlot() == -1) return false;

		print("1");

        for (int i = 0; i < inventory.Length; i++)
		{
			if (inventory[i] && inventory[i].itemName == item.itemName)
			{
				inventory[i].amt += item.amt;
                Destroy(obj);
                Destroy(pickup.item);
				return true;
			}
		}

        item.transform.parent = MGR.itemParents;
		inventory[GetNextEmptySlot()] = item;
        Destroy(obj);
        return true;
		
	}

	public int GetNextEmptySlot()
	{
        for (int i = 0; i < inventory.Length; i++)
        {
            if (!inventory[i])
            {
				return i;
            }
        }
		return -1;
    }
}
