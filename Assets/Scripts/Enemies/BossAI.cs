using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : StateMachine
{
    [SerializeField]
    List<StateTuple> standardStates, waterStates, fireStates, airStates, earthStates;

    public new enum StateOptions
    {
        MoveToPlayer,
        Attacking,
        Defending
    }


    protected void Start()
    {
        states = new Dictionary<int, State>();

        // Add all states from assigned inside the inspector
        foreach (StateTuple tuple in standardStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }

        foreach (StateTuple tuple in waterStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }

        foreach (StateTuple tuple in fireStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }

        foreach (StateTuple tuple in airStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }

        foreach (StateTuple tuple in earthStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }


        StateMachineSetup((int)startState);
    }
}
