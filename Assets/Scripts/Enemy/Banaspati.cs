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
        rigidbody.angularVelocity = Vector3.zero;

        if (distanceToAgent < 10 && State != ENEMYBEHAVIOURS.CHASE && State != ENEMYBEHAVIOURS.DEATH)
        {
            State = ENEMYBEHAVIOURS.RAGE;
        }
        else if (distanceToAgent > 15 && State != ENEMYBEHAVIOURS.ATTACK && State != ENEMYBEHAVIOURS.IDLE)
        {
            State = ENEMYBEHAVIOURS.WALK;
        }

        if (healthPoint <= 20)
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
                agent.speed = 2f;
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
                agent.speed = 4f;
                agent.SetDestination(target.transform.position);

                break;
            case ENEMYBEHAVIOURS.IDLE:
                agent.speed = 0;
                animator.SetBool("Walk", false);

                break;
            case ENEMYBEHAVIOURS.RAGE:
                agent.speed = 4.5f;
                rigidbody.velocity = Vector3.zero;
               
                if(distanceToAgent <= 5)
                {
                    agent.stoppingDistance = 3f;
                    StartCoroutine(delay());
                    IEnumerator delay()
                    {
                        agent.speed = 0;
                        yield return new WaitForSeconds(2);
                        State = ENEMYBEHAVIOURS.DEATH;
                    }

                    return;
                }
                agent.SetDestination(target.transform.position);


                break;
            case ENEMYBEHAVIOURS.DEATH:
                agent.speed = 0;
                animator.SetBool("Die", true);

                if(BanaspatiCollider.gameObject.transform.localScale.x <= 5)
                {
                    BanaspatiCollider.gameObject.transform.localScale = new Vector3(Mathf.Clamp(BanaspatiCollider.gameObject.transform.localScale.x, 0, 5), 1, 1);
                    BanaspatiCollider.gameObject.transform.localScale += Vector3.one * Time.deltaTime * 4;
                }
                Destroy(gameObject, 2);
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
