using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(Renderer))]
public class Enemy : LivingEntity {

    NavMeshAgent pathfinder;
    Transform playerTrans;
    public Player player;
    Vector3 target;
    Material myMat;

    //public GameObject weapon;
    public bool isFleeing = false;
    public float range = 0;
    public float difficulty;
    float updatePathTime = .1f;
    float lastUpdateTime = 0;
    WeaponController weaponController;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        //myMat = GetComponent<Renderer>().material;
        target = transform.position;
        weaponController = GetComponent<WeaponController>();
        range = weaponController.GetWeaponRange();
	}

    // Update is called once per frame
    void Update()
    {
        if (lastUpdateTime + updatePathTime < Time.time)
        {
            lastUpdateTime = Time.time;
            UpdateTarget();
            pathfinder.SetDestination(target);
            SetFleeingStatus();
        }

        // Draw Target Loc
        Debug.DrawLine(transform.position, target, Color.blue);

    }

    void SetFleeingStatus()
    {
        if (player.size > difficulty)
        {
            isFleeing = true;
        }
        else
        {
            isFleeing = false;
        }
    }

    void UpdateTarget()
    {
        Vector3 direction = transform.position - playerTrans.position;
        if (isFleeing)
        {
            // RUNNN AWAYY
            target = transform.position + direction.normalized * 5;
        }
        else
        {
            target = playerTrans.position + direction.normalized * range;

            if (direction.magnitude < range + .1)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        Vector3 targetPos = playerTrans.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        weaponController.Aim(playerTrans.position);
        weaponController.Attack();
    }
}
