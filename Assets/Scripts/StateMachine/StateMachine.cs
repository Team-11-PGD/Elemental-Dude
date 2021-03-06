using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected Dictionary<int, State> states = new Dictionary<int, State>();

    public int CurrentStateId { get; private set; }
    State CurrentState { get { return states[CurrentStateId]; } }

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
        if (CurrentState != null)
        {
            CurrentState.Exit();
            CurrentState.enabled = false;
        }
        CurrentStateId = stateId;

        CurrentState.SetContext(this);
        CurrentState.enabled = true;
        CurrentState.Enter();
    }
}
