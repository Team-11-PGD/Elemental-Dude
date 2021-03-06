using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float sprintSpeed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float gravityMultiplier = 1; //to adjust gravity and make it feel good
    [SerializeField]
    private float jumpGracePeroid;

    private float ySpeed;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    private CharacterController controller;
    [SerializeField]
    private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float veritcalInput = Input.GetAxisRaw("Vertical");
        float magnitude;

        Vector3 movementDirection = new Vector3(horizontalInput, 0, veritcalInput);

        //shift for sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            magnitude = Mathf.Clamp01(movementDirection.magnitude) * sprintSpeed;
        }else
        {
            magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        }
        movementDirection.Normalize();

        //moves character with camera
        float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
        //makes character move in camera direction
        movementDirection = Quaternion.Euler(0f, targetAngle, 0f)* Vector3.forward;

        //jumping
        ySpeed += gravityMultiplier * Physics.gravity.y * Time.deltaTime;
        if (controller.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpGracePeroid) 
        {
            ySpeed = -0.5f;
            if (Time.time - jumpButtonPressedTime <= jumpGracePeroid)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        controller.Move(velocity * Time.deltaTime);

    }
}
