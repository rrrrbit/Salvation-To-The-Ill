using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MGR_game : MonoBehaviour
{
	public float baseDifficulty;
	public float gameDifficulty;
	public float difficulty;

	public float timer;

	public float replenishTimer;
	public float waveTimer;
	public int wave;
	public int spawnCount;

	public List<WORLD_pickupSpawn> pickupSpawns;
	public List<WORLD_npcSpawn> spawns;


	void Start()
	{

	}

	void Update()
	{
		timer += Time.deltaTime;
		if (MGR.entities.entities.Count(x => x.team == ENTITY.Teams.ZOMBIE) <= 0) timer += Time.deltaTime;
		waveTimer -= Time.deltaTime;
		baseDifficulty = (Mathf.Sqrt(timer / 30 + 0.25f) - 0.5f);
		difficulty = gameDifficulty * wave;

		if (waveTimer < 0)
		{
			StartNewWave();
		}
	}

	void StartNewWave()
	{
		wave++;

		var halfPerDifficulty = Mathf.Pow(0.5f, (gameDifficulty - 1) / 10);
		var doublePerDifficulty = Mathf.Pow(2, (gameDifficulty - 1) / 2);
		waveTimer = BoundedCycle(wave,
			n => Mathf.Sqrt(10 * n) + 20 * halfPerDifficulty,
			n => 6 * Mathf.Sqrt(10 * n) + 100 * halfPerDifficulty,
			9 + gameDifficulty, 1.5f);

		spawnCount = BoundedCycle(wave,
			n => doublePerDifficulty * 3 * Mathf.Sqrt(n) + 1,
			n => doublePerDifficulty * 6 * Mathf.Pow(n, 0.75f), 
			9 + gameDifficulty, 1.5f);

		var leftToSpawn = spawnCount;
		while (leftToSpawn > 0)
		{
			spawns[UnityEngine.Random.Range(0, spawns.Count - 1)].leftToSpawn++;
			leftToSpawn--;
		}

		foreach (var p in pickupSpawns)
		{
			p.Replenish();
		}
	}

	int BoundedCycle(int x, Func<int, float> a, Func<int, float> b, float period, float exp) => Mathf.FloorToInt(Mathf.Lerp(a(x), b(x), Mathf.Pow(x / period % 1, exp)));
}
