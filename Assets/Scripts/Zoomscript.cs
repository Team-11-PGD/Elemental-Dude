using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Zoomscript : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera cam;
    Cinemachine3rdPersonFollow cinemachine;

    [SerializeField]
    float zoomInMax = 2f;
    [SerializeField]
    float zoomOutMax = 5f;

    // Start is called before the first frame update
    void Start()
    {
        cinemachine = cam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            cinemachine.CameraDistance -= Input.GetAxis("Mouse ScrollWheel");
            Debug.Log(Input.GetAxis("Mouse ScrollWheel"));

        }
        if(cinemachine.CameraDistance < zoomInMax)
        {
            cinemachine.CameraDistance = zoomInMax;
        }
        if(cinemachine.CameraDistance > zoomOutMax)
        {
            cinemachine.CameraDistance = zoomOutMax;
        }
    }
}
