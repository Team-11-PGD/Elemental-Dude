using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeState : State
{
    protected SlimeAI slimeAI;

    public override void Enter(int previousStateId)
    {
        slimeAI = context as SlimeAI;
    }

    public override void Exit(int nextStateId) { }
}
