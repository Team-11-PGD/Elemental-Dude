using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Funnel.Instance.funnelEvents.Add(new Funnel.FunnelEvent
            {
                name = "OnJump",
                data = new Dictionary<string, object>()
                {
                    { "Jumped", player.velocity.y > 0 }
                }
            });
        }
    }
}
