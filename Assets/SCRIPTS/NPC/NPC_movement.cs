using UnityEngine;
using UnityEngine.AI;

public class NPC_movement : Movement
{
    public Vector3 targetPos;
    public float sufficientRange;
    NavMeshPath path;

    public override void Start()
    {
        path = new();
        base.Start();
    }

    public override void FixedUpdate()
    {
        targetPos = ((NPC)entity).currentTarget.transform.position;
        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            
            if((transform.position - targetPos).sqrMagnitude > Mathf.Pow(sufficientRange, 2))
            {
                mvtIn = (Quaternion.Inverse(transform.rotation) * (path.corners[1] - transform.position)).xz().normalized;
            }
            else
            {
                mvtIn = Vector3.zero;
            }
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
        NavMesh.CalculatePath(transform.position, targetPos, NavMesh.AllAreas, path);
    }
}
