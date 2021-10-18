using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : StateMachine
{
    [SerializeField]
    StateOptions startState = StateOptions.Attacking;

    [Serializable]
    public class MyTuple : Tuple<StateOptions, State>
    {

        public StateOptions item1;
        public State item2;

        public MyTuple(StateOptions item1, State item2) : base(item1, item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }
    }
    [SerializeField]
    List<MyTuple> standardStates, waterStates, fireStates, airStates, earthStates;

    //public Dictionary<ElementMain.ElementType, List<Tuple<StateOptions, State>>> elementStates = new Dictionary<ElementMain.ElementType, List<Tuple<StateOptions, State>>>();

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
        foreach (Tuple<StateOptions, State> tuple in standardStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }

        foreach (Tuple<StateOptions, State> tuple in waterStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }

        foreach (Tuple<StateOptions, State> tuple in fireStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }

        foreach (Tuple<StateOptions, State> tuple in airStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }
        
        foreach (Tuple<StateOptions, State> tuple in earthStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }


        StateMachineSetup((int)startState);
    }
}
