using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Banaspati : EnemyManager
{
    public override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();

    }
    public override void Update()
    {
        distanceToPlayer = target.transform.position - agent.transform.position;
        distanceToAgent = distanceToPlayer.magnitude;

        if (healthPoint <= 25)
        {
            State = ENEMYBEHAVIOURS.RAGE;
        }
        else if (healthPoint <= 0)
        {
            State = ENEMYBEHAVIOURS.DEATH;
        }


        switch (State)
        {
            case ENEMYBEHAVIOURS.WALK:
                agent.speed = 5;

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(RandomLocation());
                }

                break;
            case ENEMYBEHAVIOURS.CHASE:
                agent.speed = 20;
                agent.SetDestination(target.transform.position);

                break;
            case ENEMYBEHAVIOURS.IDLE:


                break;
            case ENEMYBEHAVIOURS.RAGE:
                agent.speed = 15;
                rigidbody.velocity = Vector3.zero;
                Transform child = gameObject.transform.GetChild(0).transform;
                child.position = Vector3.MoveTowards(transform.position, Vector3.zero, 1 *Time.deltaTime);
                //agent.enabled = false;
                //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, agent.speed *Time.deltaTime);
                if(distanceToAgent <= 2)
                {
                    agent.stoppingDistance = 1f;
                    agent.velocity = Vector3.zero;
                    return;
                }
                agent.SetDestination(target.transform.position);


                break;
            case ENEMYBEHAVIOURS.DEATH:
                Debug.Log("L");

                break;
            case ENEMYBEHAVIOURS.ATTACK:


                break;
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" )
        {
            healthPoint -= 10;
            Debug.Log("Kena");
            State = ENEMYBEHAVIOURS.CHASE;
        }
    }
}
