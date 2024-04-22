using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tuyul : EnemyManager
{
    public override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        agent.speed = moveSpeed;
        agent.acceleration = acceleration;

        if (agent != null)
        {
            State = ENEMYBEHAVIOURS.WALK;
        }
    }

    public override void Update()
    {

        if(healthPoint <= 0)
        {
            State = ENEMYBEHAVIOURS.DEATH;
        }

        


        switch (State)
        {
            case ENEMYBEHAVIOURS.WALK:
                agent.speed = 4;
                acceleration = 8;
                animator.SetBool("Walk", true);
                animator.SetBool("Chase", false);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(RandomLocation());
                }

                break;
            case ENEMYBEHAVIOURS.CHASE:
                agent.speed = 5;
                animator.SetBool("Chase", true);
                animator.SetBool("Walk", false);
                agent.SetDestination(target.transform.position);
                break;
            case ENEMYBEHAVIOURS.IDLE:
                agent.speed = 0;
                break;
            case ENEMYBEHAVIOURS.RAGE:
                break;
            case ENEMYBEHAVIOURS.DEATH:
                agent.speed = 0;
                animator.SetBool("Die", true);
                break;
            case ENEMYBEHAVIOURS.ATTACK:
                break;
        }
    }
}
