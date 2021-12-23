using System;
using UnityEngine;

public class EnemyState : State
{
    protected EnemyAI enemyAI;

    public override void Enter(int previousStateId)
    {
        enemyAI = context as EnemyAI;
    }

    public override void Exit(int nextStateId) { }
}

