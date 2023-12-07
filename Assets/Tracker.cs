using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tracker : MonoBehaviour
{
    public Enemy enemy;
    private NavMeshAgent agent;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("GorillaPlayer");
        agent.destination = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (DistanceToEnemy() > 2)
        {
            agent.destination = enemy.transform.position;
            agent.isStopped = false;
            return;
        }
        else if (DistanceToEnemy() > 1)
        {
            agent.isStopped = true;
        }
        else if (agent.isStopped != false)
        {
            agent.isStopped = false;
        }
        agent.destination = target.transform.position;
    }

    private float DistanceToEnemy()
    {
        Vector3 loc = transform.position;
        Vector3 epos = enemy.transform.position;
        loc.y = 0;
        epos.y = 0;
        Vector3 diff = epos - loc;
        return diff.magnitude;
    }
}
