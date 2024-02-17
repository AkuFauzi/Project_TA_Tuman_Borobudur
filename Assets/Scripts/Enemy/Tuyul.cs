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
        agent.speed = moveSpeed;
    }

    public override void Update()
    {
        switch (State)
        {
            case ENEMYBEHAVIOURS.WALK:
                moveSpeed = 10;
                agent.SetDestination(RandomLocation());
                break;
            case ENEMYBEHAVIOURS.CHASE:
                moveSpeed = 25;
                agent.SetDestination(target.transform.position);
                break;
            case ENEMYBEHAVIOURS.IDLE:
                break;
            case ENEMYBEHAVIOURS.RAGE:
                break;
            case ENEMYBEHAVIOURS.DEATH:
                break;
            case ENEMYBEHAVIOURS.ATTACK:
                break;
        }
    }
}
