using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : StateMachine
{
    public enum StateOptions
    {
        MoveToPlayer,
        Attacking,
        Patroling,
        Heal,
        Flee
    }

    public Transform playerModel;
    public Health playerHealth;

    [SerializeField]
    List<StateTuple> inspectorStates;
    [SerializeField]
    Health enemyHealth;
    [SerializeField]
    [Range(0, 1)]
    float fleeHealthPercentage = 0.3f;

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
        foreach (StateTuple tuple in inspectorStates)
        {
            AddState(tuple.Item1, tuple.Item2);
        }

        StateMachineSetup(startState);
    }

    void Update()
    {
        if (enemyHealth.HpPercentage <= fleeHealthPercentage && (CurrentStateId != (int)StateOptions.Flee && CurrentStateId != (int)StateOptions.Heal))
        {
            TransitionTo(StateOptions.Flee);
        }
    }
}

