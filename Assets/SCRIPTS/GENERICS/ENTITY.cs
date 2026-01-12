using System.Diagnostics.Contracts;
using UnityEngine;

public class ENTITY : MonoBehaviour
{
	public GameObject obj;
	public Transform t;
	public Rigidbody rb;
	public Collider col;
	public Stats stats;
	public Item item;
	public Movement movement;
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
		item = GetComponent<Item>();
		movement = GetComponent<Movement>();

		stats.entity = this;
		item.entity = this;
		movement.entity = this;
    }
}
