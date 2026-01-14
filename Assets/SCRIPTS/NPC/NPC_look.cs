using UnityEngine;

public class NPC_look : Look
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        print(Quaternion.LookRotation((((NPC)entity).currentTarget.transform.position - transform.position).normalized).eulerAngles);
        var r = Quaternion.LookRotation((((NPC)entity).currentTarget.transform.position - transform.position).normalized).eulerAngles;
        look = new(r.y, Mathf.Clamp(-r.x, pitchClamp.x, pitchClamp.y) % 360);
        base.Update();
    }
}
