using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : LivingEntity {


    public float width, length, height;
    public GameObject FullBuilding, DeadBuilding;
    public Vector3 location;
    public float deathTime = 1.0f;
    public float population;


    protected override void Start()
    {
        base.Start();
        DeadBuilding.SetActive(false);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if(health< .5f * startingHealth)
        {
            SwapModels();
        }
    }
    
    void SwapModels()
    {
        DeadBuilding.SetActive(true);

        FullBuilding.SetActive(false);
    }


    public override void Die()
    {
        Destroy(gameObject, deathTime);

    }
}
