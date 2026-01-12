using UnityEngine;
using UnityEngine.AI;

public class NPC_movement : Movement
{
    public Transform targetPos;
    public override void FixedUpdate()
    {
        NavMeshPath path = new();
        NavMesh.CalculatePath(transform.position, targetPos.position, NavMesh.AllAreas, path);
        print(path.status);
        mvtIn = (path.corners[0].xz() - transform.position.xz()).normalized;
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        base.FixedUpdate();
    }
}
