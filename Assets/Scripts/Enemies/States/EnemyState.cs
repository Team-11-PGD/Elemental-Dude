using System;
using UnityEngine;

public class EnemyState : State
{
    protected EnemyAI enemyAI;

    public override void Enter()
    {
        enemyAI = context as EnemyAI;
    }

    public override void Exit() { }
}

