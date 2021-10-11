using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : StateMachine
{
    [SerializeField]
    StateOptions startState = StateOptions.Attacking;

    public Dictionary<State, StateOptions> dictionary = new Dictionary<State, StateOptions>();

    public enum StateOptions
    {
        MoveToPlayer,
        Attacking,
        Defending
    }

    protected void Start()
    {
        states.Add((int)StateOptions.MoveToPlayer, gameObject.AddComponent<BossMoveToPlayerState>());
        states.Add((int)StateOptions.Attacking, gameObject.AddComponent<ExampleState2>());
        states.Add((int)StateOptions.Defending, gameObject.AddComponent<ExampleState1>());

        // You also need to call the StateMachineSetup method so the statemachine works.
        // Do this after you've added all the states!
        StateMachineSetup((int)startState);
    }
}
