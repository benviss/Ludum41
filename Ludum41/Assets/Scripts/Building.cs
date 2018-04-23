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

    // Use this for initialization
    void Start()
    {
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (health < .5f * startingHealth) {
            SwapModels();
        }
    }

    void SwapModels()
    {
        FullBuilding.SetActive(false);
        DeadBuilding.SetActive(true);
    }


    public override void Die()
    {
        Destroy(gameObject, deathTime);

    }
}
