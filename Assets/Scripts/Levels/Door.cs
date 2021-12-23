using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    private bool doorClosed = true;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && doorClosed)
        {
            OpenDoor();
            doorClosed = false;
        }
    }

    public void OpenDoor()
    {
        anim.Play("DoorOpen");
    }
}
