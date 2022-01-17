using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDie : MonoBehaviour
{
    public Animator player;

    // Start is called before the first frame update
    void Start()
    {
        player.SetInteger("State", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
