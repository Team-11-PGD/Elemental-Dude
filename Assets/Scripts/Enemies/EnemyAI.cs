using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyAI : StateMachine
{
    [SerializeField]
    List<StateTuple> inspectorStates;

    protected void Start()
    {
        foreach (StateTuple tuple in inspectorStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }

        StateMachineSetup((int)startState);
    }
}

