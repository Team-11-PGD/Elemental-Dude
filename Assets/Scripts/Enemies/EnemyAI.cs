using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : StateMachine
{
    public Transform playerModel;
    public Health playerHealth;

    [SerializeField]
    Health health;
    [SerializeField]
    [Range(0, 1)]
    float fleeHealthPercentage = 0.3f;

    #region State Setup
    public enum StateOptions
    {
        MoveToPlayer,
        Attacking,
        Patroling,
        Heal,
        Flee
    }

    [SerializeField]
    protected StateOptions startState;
    [SerializeField]
    List<StateTuple> inspectorStates;

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
        foreach (StateTuple tuple in inspectorStates)
        {
            states.Add((int)tuple.Item1, tuple.Item2);
        }

        StateMachineSetup((int)startState);
    }
    #endregion State Setup

    protected virtual void OnEnable()
    {
        health.Hitted += Hitted;
        health.Died += Died;
    }

    protected virtual void OnDisable()
    {
        health.Hitted -= Hitted;
        health.Died -= Died;
    }

    protected virtual void Died() { }

    protected virtual void Hitted()
    {
        if (health.HpPercentage <= fleeHealthPercentage && (CurrentStateId != (int)StateOptions.Flee && CurrentStateId != (int)StateOptions.Heal))
        {
            TransitionTo((int)StateOptions.Flee);
        }
    }
}

