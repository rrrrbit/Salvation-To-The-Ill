using System.Diagnostics.Contracts;
using UnityEngine;

public class ENTITY : MonoBehaviour
{
	public static GameObject obj;
	public static Transform t;
	public static Rigidbody rb;
	public static Collider col;
	public static Stats stats;
	public static Item item;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
		obj = gameObject;
		t = transform;
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		stats = GetComponent<Stats>();
		item = GetComponent<Item>();
    }
}
