using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class JumpTesting : MonoBehaviour
{
    Rigidbody player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            string jumpText;
            if (player.velocity.y > 0)
            {
                jumpText = "Jumped";
            }
            else jumpText = "No jump";
            Analytics.CustomEvent(
                "Jump : ",
                 new Dictionary<string, object>() { { "JumpSpeed", jumpText } }
                 );
        }
    }
}
