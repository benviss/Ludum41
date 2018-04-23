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
    float updatePathTime = 1.0f;
    float lastUpdateTime = 0;
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

	}

    // Update is called once per frame
    void Update()
    {
        if (pathfinder.isOnNavMesh)
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
        else if (transform.position.magnitude > 10000)
        {
            Destroy(this.gameObject);
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
        if (isFleeing)
        {
            if (direction.magnitude < runDist)
            {
                // RUNNN AWAYY
                target = transform.position + direction.normalized * 5;
            }
            else
            {
                target = transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            }
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
        if (range > 50)
        {
            if (GameManager.Instance.CanAkbar())
            {
                if (Random.Range(0, 100) < 85)
                {
                    NewAudioManager.instance.Play("airhorn");
                }
                else
                {
                    NewAudioManager.instance.Play("akbar");
                }
            }
        }

        Vector3 targetPos = playerTrans.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        weaponController.Aim(playerTrans.position);
        weaponController.Attack();
    }
}
