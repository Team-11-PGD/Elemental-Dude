using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    float attackRate = 2f;
    float NextAttack;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player-Capsule");
    }

    public void Attack()
    {
        if(Time.time > NextAttack)
        {
            NextAttack = Time.time + attackRate;
            player.GetComponent<Health>().Hit(damage);
            Debug.Log("Melee");            
        }

    }

}
