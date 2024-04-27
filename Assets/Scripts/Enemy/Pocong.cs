using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pocong : EnemyManager
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

        if (healthPoint <= 50)
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
                agent.speed = 2.5f;
                animator.SetBool("Walk", true);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(RandomLocation());
                }

                break;
            case ENEMYBEHAVIOURS.CHASE:

                if (distanceToAgent <= 2)
                {
                    agent.stoppingDistance = 1f;
                    State = ENEMYBEHAVIOURS.ATTACK;
                }
                else
                {
                    agent.SetDestination(target.transform.position);
                }
                break;
            case ENEMYBEHAVIOURS.IDLE:
                agent.speed = 0;
                break;
            case ENEMYBEHAVIOURS.RAGE:
                agent.speed = 4;
                agent.SetDestination(target.transform.position);

                if (distanceToAgent <= 2)
                {
                    agent.stoppingDistance = 1f;
                    State = ENEMYBEHAVIOURS.ATTACK;
                }
                break;
            case ENEMYBEHAVIOURS.DEATH:
                agent.speed = 0;
                animator.SetBool("Die", true);
                break;
            case ENEMYBEHAVIOURS.ATTACK:
                animator.SetTrigger("Attack");

                Vector3 attackDirwolrd = target.transform.position;
                attackDirwolrd.y = transform.position.y;

                Vector3 attackDir = (attackDirwolrd - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, attackDir, Time.deltaTime);


                if (distanceToAgent >= 4)
                {
                    State = ENEMYBEHAVIOURS.CHASE;
                    animator.SetBool("Walk", true);
                }
                else if (distanceToAgent >= 4 && healthPoint <= 50)
                {
                    State = ENEMYBEHAVIOURS.RAGE;
                    animator.SetBool("Walk", true);
                }

                break;
        }
    }
}
