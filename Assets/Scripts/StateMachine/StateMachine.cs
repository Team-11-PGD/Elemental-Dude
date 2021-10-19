using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum StateOptions
    {
        MoveToPlayer,
        Attacking,
        Defending
    }

    protected Dictionary<int, State> states = new Dictionary<int, State>();

    [SerializeField]
    protected StateOptions startState = StateOptions.MoveToPlayer;

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

    State currentState = null;


    /// <summary>
    /// Set all states to inactive and activate first state
    /// </summary>
    /// <param name="startStateId"> Index to pick the first state from </param>
    protected void StateMachineSetup(int startStateId = 0)
    {
        foreach (State state in states.Values)
        {
            state.enabled = false;
        }
        TransitionTo(startStateId);
    }

    /// <summary>
    /// Change state and call the End and Start methods
    /// </summary>
    /// <param name="stateId"> New state id to change to </param>
    public void TransitionTo(int stateId)
    {
        if (currentState != null)
        {
            currentState.Exit();
            currentState.enabled = false;
        }
        currentState = states[stateId];

        currentState.SetContext(this);
        currentState.enabled = true;
        currentState.Enter();
    }
}
