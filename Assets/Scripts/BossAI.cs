using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : StateMachine
{
    [SerializeField]
    StateOptions startState = StateOptions.Attacking;

    public Dictionary<ElementMain.ElementType, List<Tuple<StateOptions, State>>> elementStates = new Dictionary<ElementMain.ElementType, List<Tuple<StateOptions, State>>>();

    public enum StateOptions
    {
        MoveToPlayer,
        Attacking,
        Defending
    }

    protected void Start()
    {
        states = new Dictionary<int, State>();

        // Add all states from assigned inside the inspector
        foreach (Tuple<StateOptions, State> tuple in elementStates[ElementMain.ElementType.None])
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }

        foreach (ElementMain.ElementType elementType in RoomGeneration.CurrentElements)
        {
            foreach (Tuple<StateOptions, State> tuple in elementStates[elementType])
            {
                states.Add((int)tuple.Item1, tuple.Item2);
            }
        }

        StateMachineSetup((int)startState);
    }
}
