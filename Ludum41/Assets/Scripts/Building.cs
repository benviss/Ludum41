﻿using System.Collections;
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
        Destroy(gameObject.GetComponent<BoxCollider>());

        SpawnPpl(peopleToSpawn);
        dead = true;
    }

    private void SpawnPpl(int num)
    {
        NewAudioManager.instance.Play("kids", transform.position, .4f);
        Spawner = Instantiate(Spawner);
        Spawner.transform.position = transform.position;
        Spawner.maxSpawnNumber = num;
        Spawner.totalSpawns = num;
    }
}
