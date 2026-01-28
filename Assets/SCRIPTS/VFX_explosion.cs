using UnityEngine;

public class VFX_explosion : MonoBehaviour
{
    ParticleSystem particle;
    Color color;

    float range;
    float speed;

    float innerLifetime;
    float innerSpeed;

    private void Awake()
    {
        

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        var outerLifetime = range / speed;

        var ptclLifetime = particle.main.startLifetime;
        ptclLifetime.constantMin = innerLifetime;
        ptclLifetime.constantMax = outerLifetime;

        var ptclSpeed = particle.main.startSpeed;
        ptclSpeed.constantMin = innerSpeed;
        ptclLifetime.constantMax = speed;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
