using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float turnSpeed = 15;
    public Transform focusPoint;
    public Transform player;

    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    [SerializeField]
    Camera mainCamera;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //camera movement
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);

        //camera and player rotation
        focusPoint.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
        player.eulerAngles = new Vector3(0, xAxis.Value, 0);

        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }
}
