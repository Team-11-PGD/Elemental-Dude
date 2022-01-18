using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    [SerializeField]
    public Health health;
    [SerializeField]
    private Object[] pickups;
    

    private int randomValue;

    private void Update()
    {
        if (health.currentHp <= 0)
        {
            ItemDrop();
        }
    }

    private void ItemDrop() 
    {
        randomValue = (int)Random.Range(0f, 6f);
        if (randomValue == 1)
        {
            Instantiate(pickups[0]);
        }
        if (randomValue == 2)
        {
            Instantiate(pickups[1]);
        }
        if (randomValue == 3)
        {
            Instantiate(pickups[2]);
        }
    }
}
