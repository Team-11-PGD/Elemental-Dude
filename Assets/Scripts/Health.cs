using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp;

   // [HideInInspector]
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
        Debug.Log($"{gameObject.name} has {currentHp} hp");

        if (currentHp <= 0)
        {
            //death
            if(gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }
            else
            {
                UIManager.instance.GoToMainMenu();
            }
        }

    }
}
