using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    float nextSpawnTime;
    int currentSpawned = 0;
    public GameObject EnemyPrefab;
    public float minSpawnTime = 0;
    public float maxSpawnTime = 0;
    public float maxSpawnNumber = 0;
    Player player;

	// Use this for initialization
	void Start () {
        nextSpawnTime = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update ()
    {
        if ((currentSpawned < maxSpawnNumber) && (Time.time > nextSpawnTime))
        {
            SpawnEnemy(Random.Range(0,player.size*2));
            nextSpawnTime = Time.time + WaitTime();
        }
	}

    private float WaitTime()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void SpawnEnemy(float difficulty)
    {
        GameObject newEnemy = Instantiate(EnemyPrefab, transform);
        Vector3 pos = Random.onUnitSphere;
        pos.y = .1f;
        newEnemy.transform.localPosition = pos;

        WeaponController weapon = newEnemy.GetComponent<WeaponController>();
        Enemy script = newEnemy.GetComponent<Enemy>();

        if (difficulty < 4)
        {
            weapon.Equipweapon(0);
        }
        else if (difficulty < 6)
        {
            weapon.Equipweapon(1);
        }
        else if (difficulty < 8)
        {
            weapon.Equipweapon(2);
        }
        else if (difficulty < 10)
        {
            weapon.Equipweapon(3);
        }

        currentSpawned++;
        script.OnDeath += OnChildDeath;
        script.player = player;
        script.difficulty = difficulty;
    }

    private void OnChildDeath()
    {
        currentSpawned--;
    }
}
