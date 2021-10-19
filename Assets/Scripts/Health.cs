using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp;

    public float currentHp;
    public float HpPercentage { get { return currentHp / maxHp; } }

    private void Start()
    {
        currentHp = maxHp;
    }

    public void Heal(int healAmt)
    {
        currentHp += healAmt;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }
    public void Hit(float damageAmt)
    {
        currentHp -= damageAmt;
        Debug.Log($"{gameObject.name} has {currentHp}");

        if (currentHp <= 0)
        {
            //death
            Debug.Log("Dead");
        }

    }
}
