using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(Renderer))]
public class Enemy : LivingEntity {

    public NavMeshAgent pathfinder;
    Transform playerTrans;
    public Player player;
    Vector3 target;
    Material myMat;

    //public GameObject weapon;
    public bool isFleeing = false;
    public float range = 0;
    public float runDist = 30;
    public float attackDist = 100;
    public float difficulty;
    float updatePathTime = .1f;
    float lastUpdateTime = 0;
    float currentWaitTime = .1f;
    WeaponController weaponController;

  public ParticleSystem DeathSplash;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        if (pathfinder == null)
        {
            pathfinder = GetComponent<NavMeshAgent>();
        }
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        //myMat = GetComponent<Renderer>().material;
        target = transform.position;
        weaponController = GetComponent<WeaponController>();
        range = weaponController.GetWeaponRange();
        StartCoroutine(Pathing());

	}

    // Update is called once per frame
    IEnumerator Pathing()
    {
        while (true)
        {
          yield return new WaitForSeconds(currentWaitTime);
            if (pathfinder.isOnNavMesh)
            {
                UpdateTarget();

                pathfinder.SetDestination(target);
                SetFleeingStatus();

                // Draw Target Loc
                Debug.DrawLine(transform.position, target, Color.blue);
            }
            else if (transform.position.magnitude > 10000)
            {
                Destroy(this.gameObject);
            }
            else
            {
                pathfinder.Warp(transform.position + Vector3.down * (1/60));
            }
        }
    }

  public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
  {
    if (damage >= health) {
      Destroy(Instantiate(DeathSplash.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, DeathSplash.startLifetime);
    }
    base.TakeHit(damage, hitPoint, hitDirection);
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
        direction.y = 0;

        currentWaitTime = (Mathf.Pow(direction.magnitude, .8f) / 8) + .1f;

        if (isFleeing)
        {
            if (direction.magnitude < runDist)
            {
                // RUNNN AWAYY
                target = transform.position + direction.normalized * 5;
            }
            else
            {
                target = transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            }
        }
        else
        {
            target = playerTrans.position + direction.normalized * range;
            target.y = 0;
            if (direction.magnitude < range + .1)
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        if (range > 50)
        {
            if (GameManager.Instance.CanAkbar())
            {
                if (Random.Range(0, 100) < 85)
                {
                    NewAudioManager.instance.Play("airhorn", transform, .7f);
                }
                //else
                //{
                //    NewAudioManager.instance.Play("akbar");
                //}
            }
        }

        Vector3 targetPos = playerTrans.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        weaponController.Aim(playerTrans.position);
        weaponController.Attack();
    }
}
