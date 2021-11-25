using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossAI : BossAI
{
    public enum StateOptions
    {
        MoveToPlayer,
        WaterAttackWave,
        WaterAttackBubble,
        WaterAttackBeam,
        WaterAttackSlam,
        MoveToCenter,
        WaterDefenceWall,
        WaterDefenceDome,
        Death
    }

    [SerializeField]
    protected StateOptions startState;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
