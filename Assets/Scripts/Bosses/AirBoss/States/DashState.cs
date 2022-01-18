using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : AirBossState
{
    public override void Enter(int previousStateId)
    {
        bossAI.SetDashPosition(bossAI.playerModel.position);
        context.NextRandomState(bossAI.CurrentStateOptions);
    }
}

