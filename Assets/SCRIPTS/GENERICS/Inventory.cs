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
	public bool drop;
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

		if (drop)
		{
			Drop(CurrentItem, -1, true);
		}

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
				if (inventory[i].amt > inventory[i].maxStack)
				{
					if(GetNextEmptySlot() == -1)
					{
						Drop(i, inventory[i].amt - inventory[i].maxStack, false);
					}
					else
					{
						var newStack = Instantiate(inventory[i].gameObject, MGR.entities.itemParents);
						newStack.GetComponent<ItemData>().amt = inventory[i].amt - inventory[i].maxStack;
						inventory[GetNextEmptySlot()] = newStack.GetComponent<ItemData>();
						inventory[i].amt = inventory[i].maxStack;
                    }
				}
				if(inventory[i].amt <= 0)
				{
					Destroy(inventory[i].gameObject);
					inventory[i] = null;
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
		if (!inventory[slot]) return;
		if(amount < 0)
		{
			amount = inventory[slot].amt;
		}

		var newPickup = Instantiate(MGR.entities.pickupPrefab);
		newPickup.transform.position = entity.look.cam.position;

        var newItem = Instantiate(inventory[slot].gameObject, newPickup.transform);
        newItem.GetComponent<ItemData>().amt = amount;

		newPickup.GetComponent<OBJ_pickup>().item = newItem;

		inventory[slot].amt -= amount;

		if (directed) newPickup.GetComponent<Rigidbody>().AddForce(entity.look.cam.transform.forward * 5 + entity.rb.linearVelocity, ForceMode.Impulse);
		else newPickup.GetComponent<Rigidbody>().AddForce(Random.insideUnitCircle.xz(0) * 5 + entity.rb.linearVelocity, ForceMode.Impulse);
    }

	public bool TryPickUp(GameObject obj)
	{
		if(!obj.TryGetComponent(out OBJ_pickup pickup) || 
			!pickup.item.TryGetComponent(out ItemData item)) return false;

		if(GetNextEmptySlot() == -1 && !inventory.Any(x => x.amt < x.maxStack))
		{
			Drop(CurrentItem, -1, false);
		}

        for (int i = 0; i < inventory.Length; i++)
		{
			if (inventory[i] && inventory[i].itemName == item.itemName)
			{
				var addAmt = Mathf.Min(item.amt, inventory[i].maxStack - inventory[i].amt);
				inventory[i].amt += addAmt;
				item.amt -= addAmt;
			}
			else if (!inventory[i])
			{
				var addAmt = Mathf.Min(item.amt, item.maxStack);
                var newItem = Instantiate(item.gameObject, MGR.entities.itemParents.transform);

                newItem.GetComponent<ItemData>().amt = addAmt;
				inventory[i] = newItem.GetComponent<ItemData>();
				item.amt -= addAmt;
            }
		}
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
