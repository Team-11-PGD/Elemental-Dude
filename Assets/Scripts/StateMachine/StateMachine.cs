using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<int, State> states = new Dictionary<int, State>();

    public int CurrentStateId { get; private set; } = -1;
    State CurrentState
    {
        get
        {
            if (CurrentStateId >= 0 && CurrentStateId < states.Count) return states[CurrentStateId];

            return null;
        }
    }

    private int EnumToInt(Enum value) => (int)Convert.ChangeType(value, value.GetTypeCode());

    /// <summary>
    /// Set all states to inactive and activate first state
    /// </summary>
    /// <param name="startStateId"> Index to pick the first state from </param>
    protected void StateMachineSetup(Enum startState)
    {
        foreach (State state in states.Values)
        {
            state.enabled = false;
        }
        TransitionTo(startState);
    }

    protected void AddState(Enum stateEnum, State stateComponent)
    {
        states.Add(EnumToInt(stateEnum), stateComponent);
    }

    /// <summary>
    /// Change state and call the End and Start methods
    /// </summary>
    /// <param name="nextStateId"> New state id to change to </param>
    public void TransitionTo(Enum nextState)
    {
        if (!enabled) return;

        int nextStateId = EnumToInt(nextState);

        if (CurrentState != null)
        {
            CurrentState.Exit(nextStateId);
            CurrentState.enabled = false;
        }
        int previousStateId = CurrentStateId;
        CurrentStateId = nextStateId;

        CurrentState.SetContext(this);
        CurrentState.enabled = true;
        CurrentState.Enter(previousStateId);
    }

    public void DisableStates()
    {
        CurrentState.enabled = false;
    }
}
