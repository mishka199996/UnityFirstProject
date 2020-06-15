using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPSController : MonoBehaviour
{
    public float patrolTime = 10f;
    public float aggroRange = 10f;
    public Transform[] waypoint;

    private int index;
    private float speed, agentSpeed;
    private Transform player;

    //private Animation anim;
    private NavMeshAgent agent;

    private void Awake()
    {
        //anim = GetComponent</Aimator>();
        agent = GetComponent<NavMeshAgent>();
        if(agent != null){agentSpeed = agent.speed;}
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoint.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        if(waypoint.Length > 0)
        {
            InvokeRepeating("Patrol", 0, patrolTime);
        }
    }

    void Patrol ()
    {
        index = index == waypoint.Length - 1 ? 0 : index + 1; 
    }

    void Tick ()
    {
        agent.destination = waypoint[index].position;
        agent.speed = agentSpeed / 2;

        if(player != null && Vector3.Distance(transform.position, player.position) <  aggroRange)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed;
        }
    }
}
