using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jm
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5.0f;
        public float rotationSpeed = 200.0f;

        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            //WASD 이동
            float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
            float vertical = Input.GetAxis("Vertical") * Time.deltaTime;

            //Shift키 확인
            bool isRunning = Input.GetKey(KeyCode.LeftShift);

            //이동 속도 조절
            float currentSpeed = isRunning ? speed * 2 : speed;
            transform.Translate(horizontal * currentSpeed, 0, vertical * currentSpeed);

            //마우스 좌우회전
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, mouseX, 0);

            //애니메이션 재생
            if (horizontal != 0 || vertical != 0)
            {
                if (isRunning)
                {
                    animator.SetBool("RunForwardBattle", true);
                    animator.SetBool("WalkForwardBattle", false);
                }
                else
                {
                    animator.SetBool("WalkForwardBattle", true);
                    animator.SetBool("RunForwardBattle", false);
                }
            }
            else
            {
                animator.SetBool("WalkForwardBattle", false);
                animator.SetBool("RunForwardBattle", false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("AttackTrigger");
            }

            if (Input.GetMouseButton(1))
            {
                animator.SetBool("isDefending", true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                animator.SetBool("isDefending", false);
            }

            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                animator.SetTrigger("ToIdle");
            }
        }
    }
}