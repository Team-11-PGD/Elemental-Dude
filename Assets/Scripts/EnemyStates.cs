using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    public enum State
    {
        Wander, Approach, Attack, Flee
    }

    public State currentState;
    GameObject enemy;

    private void Start()
    {
        enemy = this.gameObject;
        currentState = State.Wander;
    }
    public void SwitchStates(State state)
    {
        currentState = state;

        switch (state)
        {
            case State.Wander:
                enemy.GetComponent<EnemyNavControler>().Wander();
                break;
            case State.Approach:
                enemy.GetComponent<EnemyNavControler>().Approach();
                break;
            case State.Attack:
                enemy.GetComponent<EnemyNavControler>().Stop();
                enemy.GetComponent<EnemyMeleeAttack>().Attack();
                break;
            case State.Flee:
                enemy.GetComponent<EnemyNavControler>().Flee();
                break;                    
        }
    }
}
