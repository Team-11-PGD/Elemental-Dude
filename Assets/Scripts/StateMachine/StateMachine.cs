using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<int, State> states = new Dictionary<int, State>();

    public int CurrentStateId { get; private set; } = -1;

    private State CurrentState
    {
        get
        {
            if (CurrentStateId >= 0 && CurrentStateId < states.Count) return states[CurrentStateId];

            return null;
        }
    }

    /// <summary>
    /// Change the unclasified Enum to a int
    /// </summary>
    /// <param name="value"> Enum value to convert </param>
    /// <returns> The converted enum as an int </returns>
    private int EnumToInt(Enum value) => (int)Convert.ChangeType(value, value.GetTypeCode());

    /// <summary>
    /// Add a state to the state machine
    /// </summary>
    /// <param name="stateEnum"> The associated enum </param>
    /// <param name="stateComponent"> The loaded state from the scene </param>
    protected void AddState(Enum stateEnum, State stateComponent)
    {
        states.Add(EnumToInt(stateEnum), stateComponent);
    }


    /// <summary>
    /// Set all states to inactive and activate first state
    /// </summary>
    /// <param name="startState"> Index to pick the first state from </param>
    protected void StateMachineSetup(Enum startState)
    {
        foreach (State state in states.Values)
        {
            state.enabled = false;
        }
        TransitionTo(startState);
    }

    /// <summary>
    /// Change state and call the Exit and Enter methods
    /// </summary>
    /// <param name="nextState"> New state to change to </param>
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

    /// <summary>
    /// Disables the current state (does not call Exit)
    /// </summary>
    public void DisableStates()
    {
        CurrentState.enabled = false;
    }
}
