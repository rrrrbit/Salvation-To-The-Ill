
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPC : ENTITY
{

    public GameObject currentTarget;

    public Vector3 targetPosD;
    public float useRange;
	public float defaultStopRange;

    public LayerMask walls;
	public Renderer mesh;

	public NavMeshAgent agent;
	public Animator anim;

	public AnimationCurve legMvtOverSpeed;

	public enum State
	{
		idle,
		target,
		retreat
	}

	public State state;

	public override void Start()
	{
		base.Start();
		agent = GetComponent<NavMeshAgent>();
		agent.updatePosition = false;
		agent.updateRotation = false;
		agent.updateUpAxis = false;
	}

	public override void Update()
    {
        base.Update();
		UpdateAnim();



		if (team == Teams.HUMAN) mesh.material.color = Color.blue;
        else mesh.material.color = Color.red;

        if (inventory.GetCurrent().TryGetComponent(out WEAPON w)) useRange = w.stats.effectiveRange[w.Quality()];
        else useRange = inventory.GetCurrent().defaultRange;
        RecalculateTarget();

		if (currentTarget)
		{
			targetPosD = currentTarget.transform.position - transform.position;
			//((NPC_movement)movement).sufficientRange = useRange * 0.9f;
			agent.stoppingDistance = useRange*0.9f+0.5f;
			inventory.use = targetPosD.sqrMagnitude <= useRange * useRange;

			agent.destination = currentTarget.transform.position;
		}
		else
		{
			agent.destination = transform.position;
			
			targetPosD = Vector3.zero;
			inventory.use = false;
		}



		movement.mvtIn = agent.nextPosition - transform.position;
		//agent.nextPosition = transform.position;
		agent.speed = movement.xSpeed;

		for (int i = 0; i < agent.path.corners.Length - 1; i++)
		{
			Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.red);
			Debug.DrawLine(agent.path.corners[i] + Vector3.up * 2, agent.path.corners[i] - Vector3.up * 2, Color.red);
		}
	}

	void UpdateAnim()
	{
		anim.SetFloat("moving", legMvtOverSpeed.Evaluate(Vector3.Project(movement.mvtIn.xz(), rb.linearVelocity.xz()).magnitude));
	}

	void UpdateState()
	{
		var isHealingWeapon = inventory.GetCurrent() && inventory.GetCurrent().TryGetComponent(out WEAPON w) && w.stats.heal;
	}

	void RecalculateTarget()
    {
        var isHealingWeapon = inventory.GetCurrent() && inventory.GetCurrent().TryGetComponent(out WEAPON w) && w.stats.heal;
        List<ENTITY> targets = new();
        var enemies = MGR.entities.entities.Where(x => x.team != team).ToList();
        if (isHealingWeapon)
        {
            if(enemies.Count > 0) targets = enemies;
            else targets = MGR.entities.entities.Where(x=>x.stats.health < x.stats.maxHealth * 0.9f || x.stats.conversion > 0).ToList();
        }
        else targets = MGR.entities.entities.Where(x => x.team != team).ToList();
        targets.Remove(this);
        if (targets.Count <= 0)
        {
            currentTarget = null;
            if (team == Teams.HUMAN) currentTarget = PLYR.player.gameObject;
            useRange = defaultStopRange;
            return;
        }
        List<ENTITY> lineOfSight = targets.Where(x => !Physics.Linecast(transform.position, x.transform.position, walls)).ToList();
        if(lineOfSight.Count > 0) currentTarget = lineOfSight.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).First().obj;
    }
}
