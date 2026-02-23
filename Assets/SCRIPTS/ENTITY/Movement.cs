using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
	#region declarations
	Rigidbody rb;
    Collider col;
    public float xSpeed;
    [SerializeField] float xMvtLerpK;
    [SerializeField] float xMvtLerpT;
	[SerializeField] float maxStepHeight;
	[SerializeField] float minStepDepth;
	public float jumpTime;
	public float jumpHeight;
	float jumpForce;
	float grav;
	public Rigidbody groundCheck;
    public bool grounded;
	public bool lastFrameGrounded;
	public bool stairSnap;
	public bool enterCollisionTrigger;
    Vector3 horizontalVel;
	public Vector3 mvtIn;
	public bool jump;
    public ENTITY entity;

	[SerializeField] bool showIn;
	#endregion
	void Start()
    {
		jumpForce = 4f / jumpTime * jumpHeight;
		grav = -8f / jumpTime / jumpTime * jumpHeight;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
    void FixedUpdate()
    {
		rb.AddForce(Vector3.up * grav, ForceMode.Force);
		
        var targetVel = mvtIn.normalized * xSpeed;
        horizontalVel = Mathv.Lerpd(horizontalVel, targetVel, xMvtLerpK, xMvtLerpT, Time.deltaTime) ;
        rb.linearVelocity = new Vector3(horizontalVel.x, rb.linearVelocity.y, horizontalVel.z);

        RaycastHit hitInfo;
        
		if(!grounded && stairSnap) StairSnapDown();

		if (enterCollisionTrigger)
		{
			enterCollisionTrigger = false;
		}

		lastFrameGrounded = grounded;
        grounded = groundCheck.SweepTest(-transform.up, out hitInfo, 1f) && hitInfo.distance <= 0.501f;


        if(jump && grounded)
        {
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			stairSnap = false;
        }
		if(!lastFrameGrounded && grounded)stairSnap = true;

		
    }

	void Update()
	{
		if (showIn)
		{
			Debug.DrawRay(transform.position, mvtIn*2, Color.purple);
		}
	}

	void StairSnapDown()
	{
		bool shouldSnap = rb.SweepTest(Vector3.down, out var r, maxStepHeight);
		if(shouldSnap)
		{
			var height = r.point.y - transform.position.y + 1;
			if(height < 0 ) rb.MovePosition(rb.position + Vector3.up * height/2);
			grounded = true;
		}
		else
		{
			stairSnap = false;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		List<ContactPoint> points = new();
		collision.GetContacts(points);
		if(points.All(x => transform.position.y - 1 < x.point.y && x.point.y <= transform.position.y - 1 + maxStepHeight) && new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).sqrMagnitude >= 1)
		{
			var height = points.Select(x => x.point.y).Max() - transform.position.y + 1;
			rb.MovePosition(transform.position + Vector3.up * height);
			StairSnapDown();
		}
	}
}
