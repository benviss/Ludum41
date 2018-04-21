using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : MonoBehaviour {

    NavMeshAgent pathfinder;
    Transform player;
    Vector3 target;
    Material myMat;
    public bool isFleeing = false;
    public float range = 0;


	// Use this for initialization
	void Start () {
        pathfinder = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myMat = GetComponent<Material>();
        target = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        pathfinder.SetDestination(target);
    }

    void UpdateTarget()
    {
        if (isFleeing)
        {
            // RUNNN AWAYY
            target = (transform.position - player.position) *2;
        }
        else
        {
            if ((transform.position - player.position).magnitude > range)
            {
                target = Vector3.Lerp(transform.position, player.position, .8f);
            }
            else
            {
                // shoot
                myMat.color = Color.red;
            }
        }
    }
}
