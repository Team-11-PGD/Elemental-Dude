using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform focusTransform;
    public Transform movementDirection;

    public float rotationSpeedX = 5.0f;

    public float rotationSpeedY = 5.0f;

    public float maxAngle = 45;
    public float minAngle = -30;

	private void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
    }
	void Update()
    {
        movementDirection.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotationSpeedX);
        focusTransform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * rotationSpeedY);
        float x = focusTransform.localEulerAngles.x;
        if(x > 180)
        {
            x -= 360;
        }
        x = Mathf.Clamp(x, minAngle, maxAngle);

        focusTransform.rotation = Quaternion.Euler(x, movementDirection.localEulerAngles.y, 0);
    }
}
