using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSwitch : MonoBehaviour
{
    GameObject enemy;
    EnemyStates states;
    // Start is called before the first frame update
    bool aggro = false;
    void Start()
    {
        enemy = this.gameObject;
        states = enemy.GetComponent<EnemyStates>();
    }

    // Update is called once per frame
    void Update()
    {
        //enemy not triggered
        if (enemy.GetComponent<EnemyNavControler>().currentRange > enemy.GetComponent<EnemyNavControler>().noticeRange && aggro == false)
        {
            states.SwitchStates(EnemyStates.State.Wander);
        }
        //enemy has been hit
        if (enemy.GetComponent<Health>().currentHp < enemy.GetComponent<Health>().maxHp)
        {
            aggro = true;
            states.SwitchStates(EnemyStates.State.Approach);
        }
        //player gets close to enemy
        if (enemy.GetComponent<EnemyNavControler>().currentRange <= enemy.GetComponent<EnemyNavControler>().noticeRange)
        {
            aggro = true;
            states.SwitchStates(EnemyStates.State.Approach);
        }
        //enemy hp gets low
        if (enemy.GetComponent<Health>().currentHp < enemy.GetComponent<Health>().maxHp * 0.1 && aggro == true)
        {
            states.SwitchStates(EnemyStates.State.Flee);
        }
        //enemy gets close to player and is not fleeing
        if(enemy.GetComponent<EnemyNavControler>().currentRange <= enemy.GetComponent<EnemyNavControler>().stopRange && states.currentState != EnemyStates.State.Flee && aggro == true)
        {
            states.SwitchStates(EnemyStates.State.Attack);
        }
    }
}
