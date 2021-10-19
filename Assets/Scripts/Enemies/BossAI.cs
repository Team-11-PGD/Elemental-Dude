using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : StateMachine
{
    [SerializeField]
    List<StateTuple> standardStates, waterStates, fireStates, airStates, earthStates;

    public enum StateOptions
    {
        MoveToPlayer,
        Attacking,
        Defending
    }

    [SerializeField]
    protected StateOptions startState;

    [Serializable]
    protected class StateTuple : Tuple<StateOptions, State>
    {
        [SerializeField]
        StateOptions item1;
        [SerializeField]
        State item2;

        public new StateOptions Item1 { get { return item1; } }
        public new State Item2 { get { return item2; } }

        public StateTuple(StateOptions item1, State item2) : base(item1, item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }
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
