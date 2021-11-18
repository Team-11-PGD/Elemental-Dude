using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIOld : StateMachine
{
    [SerializeField]
    List<StateTuple> waterStates, fireStates, airStates, earthStates;

    public int activeStateElement = 0;
    public Transform playerModel;
    public Health playerHealth;

    public enum StateOptions
    {
        MoveToPlayer,
        FireAttacking1,
        FireAttacking2,
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

        switch (activeStateElement)
        {
            case 0:
                foreach (StateTuple tuple in waterStates)
                {
                    states.Add((int)tuple.Item1, tuple.Item2);
                }
                break;
            case 1:
                foreach (StateTuple tuple in fireStates)
                {
                    states.Add((int)tuple.Item1, tuple.Item2);
                }
                break;
            case 2:
                foreach (StateTuple tuple in airStates)
                {
                    states.Add((int)tuple.Item1, tuple.Item2);
                }
                break;
            case 3:
                foreach (StateTuple tuple in earthStates)
                {
                    states.Add((int)tuple.Item1, tuple.Item2);
                }
                break;
        }

        StateMachineSetup((int)startState);
    }
}
