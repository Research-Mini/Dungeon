using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //애니메이션 상태
    const string IDLE = "Idle";
    const string ATTACK_01 = "Attack01";
    const string ATTACK_02 = "Attack02";
    const string DIE = "Die";
    const string GETHIT = "GetHit";
    const string RUN = "Run";
    const string VICTORY = "Victory";
    const string WALK = "Walk";

    private Animator animator;
    public string currentState;

    public AudioSource awakeSound;

    // Start is called before the first frame update
    public Transform player;

    public float playerAttackDistance;

    public float AttackRate;

    private NavMeshAgent agent;

    bool isDetected = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        awakeSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (currentState == IDLE) { Stop(); }
        else if (currentState == ATTACK_01) { AttackPlayer(); }
        else if (currentState == RUN) { ChasePlayer(); }
        else if (currentState == GETHIT) { Stun(); }
        Anime();
    }

    void Anime()
    {
        //Idle 상태
        if (currentState == IDLE)
            animator.Play(IDLE);

        //Run 상태
        if (currentState == RUN)
            animator.Play(RUN);

        //Attack 상태
        if (currentState == ATTACK_01)
            animator.Play(ATTACK_01);

        //GetHit 상태
        if (currentState == GETHIT)
            animator.Play(GETHIT);

        //Victory 상태
        if (currentState == VICTORY)
            animator.Play(VICTORY);

        //Die 상태
        if (currentState == DIE)
            animator.Play(DIE);
    }

    void Stop()
    {
        agent.isStopped = true;
    }
   
    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= playerAttackDistance)
        {
            currentState = ATTACK_01;
        }
    }
    void AttackPlayer()
    {
        agent.isStopped = true;

        LookTo(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > playerAttackDistance * 1.5f)
        {
            currentState = RUN;
        }
    }

    void Stun()
    {
        agent.isStopped = true;
        currentState = GETHIT;
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.position);
        directionToPosition.y = 0;
        transform.forward = directionToPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isDetected = true;
            player = other.gameObject.transform;
            currentState = RUN;
            awakeSound.enabled = true;
        }
    }

}
