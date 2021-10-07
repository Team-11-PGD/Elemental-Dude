using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp;
    public float currentHp;

    private void Start()
    {
        currentHp = maxHp;
    }

    public void Hit(float damageAmt)
    {
        currentHp -= damageAmt;

        if (currentHp <= 0)
        {
            //death
        }

    }
}
