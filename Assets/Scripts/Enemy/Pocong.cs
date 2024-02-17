using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pocong : EnemyManager
{
    public override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    public override void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= 5)
        {
            Debug.Log("Jalan");
            State = ENEMYBEHAVIOURS.WALK;
            
        }
        /*int randomenum = Random.Range(0, 5);
        switch (randomenum)
        {
            case 0: State = ENEMYBEHAVIOURS.WALK; break; 
            case 1: State = ENEMYBEHAVIOURS.CHASE; break; 
            case 2: State = ENEMYBEHAVIOURS.IDLE; break; 
            case 3: State = ENEMYBEHAVIOURS.RAGE; break; 
            case 4: State = ENEMYBEHAVIOURS.DEATH; break; 
        }*/
        switch (State)
        {
            case ENEMYBEHAVIOURS.WALK:
                Debug.Log("Mulai");
                moveSpeed = 5;
                agent.SetDestination(RandomLocation());
                break;
            case ENEMYBEHAVIOURS.CHASE:
                Debug.Log("Kejar");
                moveSpeed = 20;
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




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Kena");
            State = ENEMYBEHAVIOURS.CHASE;
        }
    }
}
