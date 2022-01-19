using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public Transform movementDirection;
    [SerializeField]
    public CharacterController controller;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float sprintSpeed = 10f;
    [SerializeField]
    private float jumpHeight = 1f;
    [SerializeField]
    private float gravity = -9.81f;

    public Vector3 velocity;

    public bool freezeWalkingInput;

    void Update()
    {
        // Walking
        Vector3 walkingMovement = Input.GetAxis("Vertical") * movementDirection.right + Input.GetAxis("Horizontal") * movementDirection.forward;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkingMovement *= sprintSpeed * Time.deltaTime;
        }
        else
        {
            walkingMovement *= speed * Time.deltaTime;
        }
        velocity = new Vector3(0, velocity.y, 0);
        velocity += walkingMovement;


        // Jumping
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity) * Time.deltaTime;
        }
        velocity.y += gravity * Time.deltaTime * Time.deltaTime;


        //Stop player from moving
        if (!freezeWalkingInput)
        {
            //Player movement
            controller.Move(velocity);
        }

        if (freezeWalkingInput)
        {
            //Debug.Log("Player frozen");
        }
    }
}

