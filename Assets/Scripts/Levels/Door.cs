using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject leftDoor, rightDoor;

    bool test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public IEnumerator openDoor(GameObject door)
    {
        yield return new WaitUntil(() => door.transform.rotation.y => 93f);
    }*/
}
