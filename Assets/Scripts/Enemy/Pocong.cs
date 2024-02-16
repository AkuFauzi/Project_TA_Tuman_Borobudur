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
    }
}
