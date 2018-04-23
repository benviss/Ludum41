using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : LivingEntity
{

    Rigidbody building;


    public float width, length, height;
    public GameObject FullBuilding, DeadBuilding;
    public Vector3 location;
    public float deathTime = 1.0f;
    public GameObject dyingBuildingEffects;
    public GameObject deadBuildingEffects;
    public int peopleToSpawn;
    public EnemySpawner Spawner;


    // Use this for initialization
    void Start()
    {
        DeadBuilding.SetActive(false);
        base.Start();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (health < .5f * startingHealth && health > 0) {
            SwapModels();
        }
    }

    void SwapModels()
    {
        if (!DeadBuilding.active) {
            Instantiate(dyingBuildingEffects, DeadBuilding.transform);
        }
        FullBuilding.SetActive(false);
        DeadBuilding.SetActive(true);
    }


    public override void Die()
    {
        Destroy(Instantiate(deadBuildingEffects, DeadBuilding.transform.position, DeadBuilding.transform.rotation) as GameObject,10);
        
        Destroy(gameObject, deathTime);

        SpawnPpl(peopleToSpawn);
    }

    private void SpawnPpl(int num)
    {
        Spawner = Instantiate(Spawner, transform);
        Spawner.transform.position = Vector3.up * 10 +transform.position;
        Spawner.maxSpawnNumber = num;
        Spawner.totalSpawns = num;
        Destroy(Spawner, 1f);
    }
}
