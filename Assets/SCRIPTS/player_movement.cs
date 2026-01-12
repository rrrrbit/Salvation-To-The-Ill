using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    InpActions.PlayerActions actions;
    Rigidbody rb;
    Collider col;

    [SerializeField] float xSpeed;
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

    void Start()
    {
		jumpForce = 4f / jumpTime * jumpHeight;
		grav = -8f / jumpTime / jumpTime * jumpHeight;

		var inp = new InpActions();
        inp.Enable();
        actions = inp.Player;
        actions.Enable();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		rb.AddForce(Vector3.up * grav, ForceMode.Force);
		var mvtIn = actions.move.ReadValue<Vector2>();
        var targetVel = transform.rotation * new Vector3(mvtIn.x, 0, mvtIn.y) * xSpeed;
        horizontalVel = GLOBAL.Lerpd(horizontalVel, targetVel, xMvtLerpK, xMvtLerpT, Time.deltaTime) ;
        rb.linearVelocity = new Vector3(horizontalVel.x, rb.linearVelocity.y, horizontalVel.z);

        RaycastHit hitInfo;
        
		if(!grounded && stairSnap) StairSnapDown();

		if (enterCollisionTrigger)
		{
			enterCollisionTrigger = false;
		}

		lastFrameGrounded = grounded;
        grounded = groundCheck.SweepTest(-transform.up, out hitInfo, 1f) && hitInfo.distance <= 0.501f;


        if(actions.Jump.IsPressed() && grounded)
        {
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			stairSnap = false;
        }
		if(!lastFrameGrounded && grounded)stairSnap = true;

    }

	void StairSnapDown()
	{
		bool shouldSnap = rb.SweepTest(Vector3.down, out var r, maxStepHeight);
		if(shouldSnap)
		{
			var height = r.point.y - transform.position.y + 1;
			print(height);
			rb.MovePosition(rb.position + Vector3.up * height/2);
		}
		else
		{
			stairSnap = false;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		List<ContactPoint> points = new();
		collision.GetContacts(points);
		if(points.All(x => transform.position.y - 1 < x.point.y && x.point.y <= transform.position.y - 1 + maxStepHeight) && new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).sqrMagnitude <= 1)
		{
			var height = points.Select(x => x.point.y).Max() - transform.position.y + 1;
			rb.MovePosition(transform.position + Vector3.up * height);
		}
		StairSnapDown();
	}
}
