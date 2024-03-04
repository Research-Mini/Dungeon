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
    private float maxHealth = 100f; // 가정한 최대 체력 값
    private string currentState;
    private bool hasScreamed = false; // Scream이 실행되었는지 추적하는 플래그

    // 상태를 문자열로 정의
    private const string IDLE = "Idle01";
    private const string WALK = "Walk";
    private const string ATTACK_01 = "Basic Attack"; // Animator 내의 실제 애니메이션 이름과 일치해야 함
    private const string ATTACK_02 = "Claw Attack"; // Animator 내의 실제 애니메이션 이름과 일치해야 함
    private const string SCREAM = "Scream"; // 추가된 상태

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
        yield return new WaitForSeconds(1f); // 수정: 대기 시간을 실제 Scream 애니메이션 길이로 조정
    }

    void ChangeState(string newState)
    {
        if (currentState != newState)
        {
            animator.Play(newState);
            currentState = newState;

            // 공격 상태일 때 이동 정지
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

    // Gizmos를 사용하여 인식 범위와 공격 범위를 시각적으로 표시
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // 인식 범위의 색상을 노란색으로 설정
        Gizmos.DrawWireSphere(transform.position, detectionRange); // 인식 범위를 나타내는 와이어프레임 구체를 그림

        Gizmos.color = Color.red; // 공격 범위의 색상을 빨간색으로 설정
        Gizmos.DrawWireSphere(transform.position, attackRange); // 공격 범위를 나타내는 와이어프레임 구체를 그림
    }
}
