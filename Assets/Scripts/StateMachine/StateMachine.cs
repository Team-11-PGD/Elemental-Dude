using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public int CurrentStateId { get; private set; } = -1;

    Dictionary<int, State> states = new Dictionary<int, State>();

    State CurrentState
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
    /// <param name="startState"> Enum to pick the first state from </param>
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
    /// Choose a random state
    /// </summary>
    /// <param name="stateOptions"> Next state options </param>
    /// <param name="excludeStateOptions"> When true all options appart from stateOptions can be choosen</param>
    public void NextRandomState(Enum[] stateOptions, bool excludeStateOptions = false)
    {
        List<Enum> enums = new List<Enum>();
        if (!excludeStateOptions)
        {
            // Use given options
            enums = stateOptions.ToList();
        }
        else
        {
            // Find and use all options that were not given
            Array allOptions = stateOptions[0].GetType().GetEnumValues();
            for (int i = 0; i < allOptions.Length; i++)
            {
                if (!stateOptions.Contains(allOptions.GetValue(i)))
                {
                    enums.Add((Enum)allOptions.GetValue(i));
                }
            }
        }

        // Check if there was at least one option
        if (enums.Count == 0) throw new Exception("No state was found to switch to. This can be caused by an empty Enum or excluding all options");

        // Transition to a random option
        TransitionTo(enums[UnityEngine.Random.Range(0, enums.Count)]);
    }

    /// <summary>
    /// Choose a random state with Enum type
    /// </summary>
    /// <typeparam name="T"> Can only be a Enum </typeparam>
    public void NextRandomState<T>()
    {
        Type type = typeof(T);

        // See if Enum type was given
        if (!type.IsEnum) throw new Exception("T must be a Enum");

        // Find all options
        Array enums = Enum.GetValues(type);

        // Transition to a random option
        TransitionTo((Enum)enums.GetValue(UnityEngine.Random.Range(0, enums.Length)));
    }

    /// <summary>
    /// Disables the current state (does not call Exit)
    /// </summary>
    public void DisableStates()
    {
        CurrentState.enabled = false;
    }
}
