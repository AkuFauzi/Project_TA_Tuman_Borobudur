using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyManager : MonoBehaviour
{
    public enum ENEMYBEHAVIOURS
    {
        WALK,
        CHASE,
        IDLE,
        RAGE,
        DEATH,
        ATTACK
    }
    public ENEMYBEHAVIOURS State;
    public ENEMYBEHAVIOURS GetState() { return State; }
    public float walkRadius;
    public float distanceToAgent;
    public int healthPoint;
    public int attackPoint;
    public GameObject target;
    public NavMeshAgent agent;
    public HealthBar healthBar;
    public Vector3 distanceToPlayer;
    public Collider BanaspatiCollider;
    public Rigidbody rigidbody;
    public Animator animator;
    public GameObject rageEffect;

    public bool cdState;

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    public virtual void Update()
    {

    }

    public virtual Vector3 RandomLocation()
    {
        Vector3 finalpostion = Vector3.zero;
        Vector3 randomposition = Random.insideUnitSphere * walkRadius;
        randomposition += transform.position;
        if(NavMesh.SamplePosition(randomposition, out NavMeshHit hit, walkRadius, 1))
        {
            finalpostion = hit.position;
        }
        return finalpostion;
        
    }
}
