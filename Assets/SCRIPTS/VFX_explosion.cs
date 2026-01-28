using UnityEngine;

public class VFX_explosion : MonoBehaviour
{
    ParticleSystem particle;

    public float range;
    public float speed;

    public float innerLifetime;
    public float innerSpeed;


    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        var outerLifetime = range / speed;

        innerLifetime = outerLifetime * 0.1f;
        innerSpeed = speed * 0.9f;

        var ptcl = particle.main;

        ptcl.startLifetime = new(innerLifetime, outerLifetime);
        ptcl.startSpeed = new(innerSpeed, speed);
        
    }
}
