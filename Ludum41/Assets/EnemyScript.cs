using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Renderer))]
public class EnemyScript : MonoBehaviour {

    NavMeshAgent pathfinder;
    Transform player;
    public Vector3 target;
    Material myMat;
    public bool isFleeing = false;
    public float range = 0;

    float updatePathTime = .1f;
    float lastUpdateTime = 0;


	// Use this for initialization
	void Start () {
        pathfinder = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myMat = GetComponent<Renderer>().material;
        target = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        if (lastUpdateTime + updatePathTime < Time.time)
        {
            lastUpdateTime = Time.time;
            UpdateTarget();
            pathfinder.SetDestination(target);
        }

        // Draw Target Loc
        Debug.DrawLine(transform.position, target, Color.blue);
    }

    void UpdateTarget()
    {
        Vector3 direction = transform.position - player.position;
        if (isFleeing)
        {
            // RUNNN AWAYY
            target = transform.position + direction.normalized * 5;
        }
        else
        {
            target = player.position + direction.normalized * range;

            if (direction.magnitude < range + .1)
            {
                // shoot
                myMat.color = Color.blue;
                isFleeing = true;
            }
        }
    }
}
