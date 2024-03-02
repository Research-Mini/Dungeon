using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jm
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5.0f; 
        public float rotationSpeed = 200.0f; 
        public Transform cameraTransform; 
        public float maxViewAngle = 60.0f; 

        private Animator animator;
        private Rigidbody rb; 
        private float verticalLookRotation = 0;

        void Start()
        {
            animator = GetComponent<Animator>(); 
            rb = GetComponent<Rigidbody>(); 
            Cursor.lockState = CursorLockMode.Locked;

           
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, mouseX, 0);

            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            verticalLookRotation += mouseY; 
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -maxViewAngle, maxViewAngle);
            cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

            HandleAnimation();
        }

        void FixedUpdate()
        {
            FreezeRotation();
            HandleMovement();
        }

        void FreezeRotation()
        {
            rb.angularVelocity = Vector3.zero;
        }
        void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal") * speed;
            float vertical = Input.GetAxis("Vertical") * speed;

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            Vector3 movementDirection = new Vector3(horizontal, 0, vertical).normalized;
            Vector3 movement = transform.TransformDirection(movementDirection) * (isRunning ? speed * 2 : speed) * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + movement);
        }

        void HandleAnimation()
        {
            bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
            bool isRunning = Input.GetKey(KeyCode.LeftShift);

            if (isMoving)
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