using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject fireSlam;
    public Transform boss;
    public enum bossAttacks
    {
        fireBreath,
        fireSlam,
        fireballRain,
        lavaStream
    }
    public bossAttacks currentState;

    public void DoAttack()
    {
        if (currentState == bossAttacks.fireSlam)
        {
            // TODO: SOUND(fire slam)
            Instantiate(fireSlam, boss.position, boss.rotation);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
        
            DoAttack();
        
    }
}
