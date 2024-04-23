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

        if (agent != null)
        {
            State = ENEMYBEHAVIOURS.WALK;
        }
    }

    public override void Update()
    {
        distanceToPlayer = target.transform.position - agent.transform.position;
        distanceToAgent = distanceToPlayer.magnitude;

        if(healthPoint <= 50)
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
                agent.speed = 4;
                animator.SetBool("Walk", true);
                animator.SetBool("Chase", false);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(RandomLocation());
                }

                break;
            case ENEMYBEHAVIOURS.CHASE:
                agent.speed = 10;
                animator.SetBool("Chase", true);
                animator.SetBool("Walk", false);
                agent.SetDestination(target.transform.position);

                if (distanceToAgent <= 1)
                {
                    State = ENEMYBEHAVIOURS.ATTACK;
                }
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
                animator.SetBool("Chase", false);
                animator.SetTrigger("Attack");


                if(distanceToAgent >= 1)
                {
                    State = ENEMYBEHAVIOURS.CHASE;
                    animator.SetBool("Chase", true);
                }
                else if (distanceToAgent >= 1 && healthPoint <= 50)
                {
                    State = ENEMYBEHAVIOURS.RAGE;
                    animator.SetBool("Chase", true);
                }

                break;
        }
    }
}
