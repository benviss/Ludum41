using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    List<GameObject> enemies;
    float nextSpawnTime;

    public GameObject EnemyPrefab;
    public float minSpawnTime = 0;
    public float maxSpawnTime = 0;
    public float maxSpawnNumber = 0;

	// Use this for initialization
	void Start () {
        enemies = new List<GameObject>();
        nextSpawnTime = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((enemies.Count < maxSpawnNumber) && (Time.time > nextSpawnTime))
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + WaitTime();
        }
	}

    private float WaitTime()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(EnemyPrefab, transform);
        Vector3 pos = Random.onUnitSphere;
        pos.y = .1f;
        newEnemy.transform.localPosition = pos;
    }
}
