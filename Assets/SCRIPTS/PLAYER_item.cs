using TMPro;
using UnityEditor;
using UnityEngine;

public class PLAYER_item : MonoBehaviour
{
	InpActions.PlayerActions actions;
	public Transform useOrigin;
	public float shootTimer;
	public float useTimer;

	public ItemData[] inventory;
	public int CurrentItem
	{
		get { return currentItem; }
        set { currentItem = (value + 5) % inventory.Length; }
    }
	int currentItem;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		var inp = new InpActions();
		inp.Enable();
		actions = inp.Player;
		actions.Enable();

		//inventory = new GameObject[5];
	}

    // Update is called once per frame
    void Update()
    {
		shootTimer = Mathf.Max(0, shootTimer - Time.deltaTime);
		useTimer = Mathf.Max(0, useTimer - Time.deltaTime);


		if (actions.shoot.IsPressed() && shootTimer <= 0 && useTimer <= 0)
		{
			UseBehaviour useBehaviour;
			if (inventory[CurrentItem] && inventory[CurrentItem].TryGetComponent(out useBehaviour))
			{
				useBehaviour.TryUse();
			}
		}

		if (actions.scrollForward.WasPerformedThisFrame()) CurrentItem--;
		if (actions.scrollBackward.WasPerformedThisFrame()) CurrentItem++;
    }
}
