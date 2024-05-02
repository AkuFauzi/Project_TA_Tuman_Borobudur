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
        rigidbody = GetComponent<Rigidbody>();

        if (agent != null)
        {
            State = ENEMYBEHAVIOURS.WALK;
        }
    }

    public override void Update()
    {
        distanceToPlayer = target.transform.position - agent.transform.position;
        distanceToAgent = distanceToPlayer.magnitude;

        rigidbody.velocity = Vector3.zero;
        if (healthPoint <= 0)
        {
            Debug.Log("D");
            State = ENEMYBEHAVIOURS.DEATH;
        }
        else if (healthPoint <= 50)
        {
            State = ENEMYBEHAVIOURS.RAGE;
        }
        



        switch (State)
        {
            case ENEMYBEHAVIOURS.WALK:
                agent.speed = 3.8f;
                animator.SetBool("Walk", true);
                animator.SetBool("Chase", false);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(RandomLocation());
                }

                break;
            case ENEMYBEHAVIOURS.CHASE:
                agent.speed = 7;
                animator.SetBool("Chase", true);
                animator.SetBool("Walk", false);

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
                agent.speed = 10;
                animator.SetBool("Chase", true);
                animator.SetBool("Walk", false);
                agent.SetDestination(target.transform.position);

                if (distanceToAgent <= 1)
                {
                    State = ENEMYBEHAVIOURS.ATTACK;
                }
                break;
            case ENEMYBEHAVIOURS.DEATH:
                agent.speed = 0;
                animator.SetBool("Die", true);
                break;
            case ENEMYBEHAVIOURS.ATTACK:
                animator.SetBool("Chase", false);
                animator.SetTrigger("Attack");

                Vector3 attackDirwolrd = target.transform.position;
                attackDirwolrd.y = transform.position.y;

                Vector3 attackDir = (attackDirwolrd - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, attackDir, Time.deltaTime);


                if (distanceToAgent >= 4)
                {
                    State = ENEMYBEHAVIOURS.CHASE;
                    animator.SetBool("Chase", true);
                }
                else if (distanceToAgent >= 4 && healthPoint <= 50)
                {
                    State = ENEMYBEHAVIOURS.RAGE;
                    animator.SetBool("Chase", true);
                }

                break;
        }
    }
}