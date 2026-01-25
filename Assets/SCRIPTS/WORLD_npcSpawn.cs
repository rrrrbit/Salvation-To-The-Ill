using UnityEditor;
using UnityEngine;

public class WORLD_npcSpawn : MonoBehaviour
{
    public int leftToSpawn;

    public float timer;
    public float spawnInterval;
	public float spawnRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MGR.game.spawns.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        timer = Mathf.Max(0, timer - Time.deltaTime);

        if(timer <= 0)
        {
            timer = spawnInterval;
            if(leftToSpawn > 0)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        var thisNpc = MGR.entities.RandomNPC(transform.position + (Random.insideUnitCircle * spawnRange).xz(0));
        leftToSpawn--;
    }

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, spawnRange);
	}
}
