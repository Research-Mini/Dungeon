using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 5f;
    public float healthPercentageForClawAttack = 70f;

    private float health;
    private float maxHealth = 100f; // ������ �ִ� ü�� ��
    private string currentState;
    private bool hasScreamed = false; // Scream�� ����Ǿ����� �����ϴ� �÷���

    // ���¸� ���ڿ��� ����
    private const string IDLE = "Idle01";
    private const string WALK = "Walk";
    private const string ATTACK_01 = "Basic Attack"; // Animator ���� ���� �ִϸ��̼� �̸��� ��ġ�ؾ� ��
    private const string ATTACK_02 = "Claw Attack"; // Animator ���� ���� �ִϸ��̼� �̸��� ��ġ�ؾ� ��
    private const string SCREAM = "Scream"; // �߰��� ����

    void Start()
    {
        health = maxHealth; 
        currentState = IDLE;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer <= attackRange)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= 15f)
            {
                if ((health / maxHealth) * 100f > healthPercentageForClawAttack)
                {
                    ChangeState(ATTACK_01);
                }
                else
                {
                    ChangeState(ATTACK_02);
                }
            }
        }
        else
        {
            ChangeState(WALK);
            agent.SetDestination(player.position);
        }
    }

    IEnumerator DoScream()
    {
        ChangeState(SCREAM);
        hasScreamed = true;
        yield return new WaitForSeconds(1f); // ����: ��� �ð��� ���� Scream �ִϸ��̼� ���̷� ����
    }

    void ChangeState(string newState)
    {
        if (currentState != newState)
        {
            animator.Play(newState);
            currentState = newState;

            // ���� ������ �� �̵� ����
            if (newState == ATTACK_01 || newState == ATTACK_02)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
    
}

    // Gizmos�� ����Ͽ� �ν� ������ ���� ������ �ð������� ǥ��
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // �ν� ������ ������ ��������� ����
        Gizmos.DrawWireSphere(transform.position, detectionRange); // �ν� ������ ��Ÿ���� ���̾������� ��ü�� �׸�

        Gizmos.color = Color.red; // ���� ������ ������ ���������� ����
        Gizmos.DrawWireSphere(transform.position, attackRange); // ���� ������ ��Ÿ���� ���̾������� ��ü�� �׸�
    }
}
