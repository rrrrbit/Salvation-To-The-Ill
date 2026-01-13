using UnityEngine;
using UnityEngine.AI;

public class NPC_movement : Movement
{
    public Transform targetPos;
    NavMeshPath path;

    public override void Start()
    {
        path = new();
        base.Start();
    }

    public override void FixedUpdate()
    {
        
        if(path.status != NavMeshPathStatus.PathInvalid)
        {
            mvtIn = (path.corners[1].xz() - transform.position.xz()).normalized;
        } 
        else
        {
          
        }

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            Debug.DrawLine(path.corners[i] + Vector3.up * 2, path.corners[i] - Vector3.up * 2, Color.red);
        }
            
        base.FixedUpdate();
    }

    public void RecalcPath()
    {
        NavMesh.CalculatePath(transform.position, targetPos.position, NavMesh.AllAreas, path);
    }
}
