using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform movementDirection;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float jumpHeight = 1f;
    [SerializeField]
    private float gravity = -9.81f;

    private Vector3 velocity;

    public bool freezeWalkingInput;

    void Update()
    {
        // Walking
        Vector3 walkingMovement = Input.GetAxis("Horizontal") * movementDirection.right + Input.GetAxis("Vertical") * movementDirection.forward;
        walkingMovement *= Time.deltaTime * speed;

        // Jumping
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        //Stop player from moving
        if (!freezeWalkingInput)
        {
            //Player movement
            controller.Move(velocity * Time.deltaTime + walkingMovement);
        }

        if (freezeWalkingInput)
        {
            //Debug.Log("Player frozen");
        }
    }
}
