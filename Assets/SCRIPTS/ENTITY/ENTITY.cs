using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Audio;

public class ENTITY : MonoBehaviour
{
	public GameObject obj;
	public Transform t;
	public Rigidbody rb;
	public Collider col;
	public Stats stats;
	public Inventory inventory;
	public Movement movement;
	public Look look;
	public new Audio audio;
	public enum Teams {
		HUMAN,
		ZOMBIE,
	}
	public Teams team;

    public virtual void Start()
    {
		obj = gameObject;
		t = transform;
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		stats = GetComponent<Stats>();
		inventory = GetComponent<Inventory>();
		movement = GetComponent<Movement>();
		look = GetComponent<Look>();
		audio = GetComponent<Audio>();

		stats.entity = this;
		inventory.entity = this;
		movement.entity = this;
		look.entity = this;

		MGR.entities.entities.Add(this);
    }

    public virtual void Update()
    {
		transform.localScale = Vector3.one * stats.size;
		movement.xSpeed = stats.speed / stats.size;
    }

    private void OnDestroy()
    {
        MGR.entities.entities.Remove(this);
    }
}
