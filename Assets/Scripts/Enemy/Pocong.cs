using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Pocong : EnemyManager
{
    public override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        rageEffect.SetActive(false);

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

        if (distanceToAgent < 10 && State != ENEMYBEHAVIOURS.CHASE && State != ENEMYBEHAVIOURS.ATTACK)
        {
            State = ENEMYBEHAVIOURS.CHASE;
        }

        if (healthPoint <= 0)
        {

            State = ENEMYBEHAVIOURS.DEATH;
        }
        else if (healthPoint <= 50)
        {
            State = ENEMYBEHAVIOURS.RAGE;
        }




        switch (State)
        {
            case ENEMYBEHAVIOURS.WALK:
                agent.speed = 2.5f;
                animator.SetBool("Walk", true);

                if (agent.remainingDistance <= 1)
                {
                    if (cdState) return;
                    bool isIdle = false;

                    int rnd = Random.RandomRange(0, 2);
                    if (rnd == 1) isIdle = true;
                    if (isIdle)
                    {
                        StartCoroutine(delay());
                        IEnumerator delay()
                        {
                            State = ENEMYBEHAVIOURS.IDLE;
                            cdState = true;
                            yield return new WaitForSeconds(Random.RandomRange(3, 5));
                            State = ENEMYBEHAVIOURS.WALK;
                            cdState = false;
                        }
                    }
                    else
                    {
                        agent.SetDestination(RandomLocation());
                    }

                }





                break;
            case ENEMYBEHAVIOURS.CHASE:

                animator.SetBool("Walk", true);
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
                animator.SetBool("Walk", false);
                break;
            case ENEMYBEHAVIOURS.RAGE:
                agent.speed = 4;
                animator.SetBool("Walk", true);
                rageEffect.SetActive(true);
                agent.SetDestination(target.transform.position);

                if (distanceToAgent <= 2)
                {
                    agent.stoppingDistance = 1f;
                    State = ENEMYBEHAVIOURS.ATTACK;
                }
                if (healthPoint == 0)
                {
                    State = ENEMYBEHAVIOURS.DEATH;
                }
                break;
            case ENEMYBEHAVIOURS.DEATH:
                agent.speed = 0;
                animator.SetBool("Die", true);
                enemyCollider.enabled = false;
                rigidbody.angularVelocity = Vector3.zero;
                Destroy(gameObject, 2);


                break;
            case ENEMYBEHAVIOURS.ATTACK:
                Debug.Log("Attack");
                animator.SetBool("Walk", false);
                animator.SetTrigger("Attack");

                Vector3 attackDirwolrd = target.transform.position;
                attackDirwolrd.y = transform.position.y;

                Vector3 attackDir = (attackDirwolrd - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, attackDir, Time.deltaTime);

                if (distanceToAgent >= 4)
                {
                    Debug.Log("distanceToAgent");
                    State = ENEMYBEHAVIOURS.CHASE;
                    animator.SetBool("Walk", true);
                }
                else if (distanceToAgent >= 4 && healthPoint <= 50)
                {
                    State = ENEMYBEHAVIOURS.RAGE;
                    animator.SetBool("Walk", true);
                }

                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            healthPoint -= 10;
        }
    }
}
