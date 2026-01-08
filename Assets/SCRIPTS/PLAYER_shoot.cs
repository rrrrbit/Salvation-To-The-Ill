using UnityEditor;
using UnityEngine;

public class PLAYER_shoot : MonoBehaviour
{
	InpActions.PlayerActions actions;
	public Transform origin;
	public float shootTimer;
	public float useTimer;

	public GameObject[] inventory;
	public int currentItem;

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


		if (actions.shoot.IsPressed() && shootTimer <= 0)
		{
			UseBehaviour useBehaviour;
			if (inventory[currentItem].TryGetComponent<UseBehaviour>(out useBehaviour))
			{
				useBehaviour.TryUse();
			}
		}
    }
}
