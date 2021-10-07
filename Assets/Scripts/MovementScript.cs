using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 5f;
    public float sprintSpeed = 10f;
    public float gravity = -9.81f;
    public float turnTime = 0.1f;
    public float jumpHeight = 10f;
    float turnVelocity;

    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        //2d movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //camera movement
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
        transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);

        if (direction.magnitude >= 0.1f)
        {
            //sprinting
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(moveDirection * sprintSpeed * Time.deltaTime);
            }
            else
            {
                controller.Move(moveDirection * speed * Time.deltaTime);
            }
        }
        //jumping
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            Debug.Log("yo");
            velocity.y += Mathf.Sqrt(jumpHeight * 3f * -gravity) * Time.deltaTime;
        }
        velocity.y += gravity * Time.deltaTime * Time.deltaTime;




        controller.Move(velocity);

    }
}
