using UnityEngine;

public class NPC_look : Look
{
    public override void Update()
    {
        float yaw = Quaternion.LookRotation(((NPC)entity).targetPosD).eulerAngles.y;
        float pitch = Vector3.SignedAngle(transform.forward, ((NPC)entity).targetPosD, transform.right);
        look = new(yaw, pitch);
        base.Update();
    }
}
