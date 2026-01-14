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
        var r = Quaternion.LookRotation((((NPC)entity).currentTarget.transform.position - transform.position).normalized, Vector3.up).eulerAngles;
        look = new(r.y,-r.x);
        base.Update();
    }
}
