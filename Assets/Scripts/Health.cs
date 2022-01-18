using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ElementMain))]
public class Health : MonoBehaviour
{
    [SerializeField]
    public GameObject dmgText;
    public float maxHp;
    // [HideInInspector]
    public float currentHp;
    public float damageTaken;

    public event Action Hitted;
    public event Action Died;
    public event Action Healed;

    public float HpPercentage { get { return currentHp / maxHp; } }

    void Start()
    {
        currentHp = maxHp;
    }

    public void Heal(int healAmt)
    {
        currentHp += healAmt;
        if (currentHp > maxHp) currentHp = maxHp;
        Healed?.Invoke();
    }

    public void Hit(float damageAmt)
    {
        if(dmgText != null)
        {
            ShowDmgText();
        }
        damageTaken = damageAmt;
        currentHp -= damageAmt;
        Debug.Log($"{gameObject.name} has {currentHp} hp and had {currentHp + damageAmt}");

        Hitted?.Invoke();
        if (currentHp <= 0)
        {
            if (currentHp + damageAmt > 0)
            {
                Died?.Invoke();
            }
            currentHp = 0;
        }
    }

    void ShowDmgText()
    {
        Instantiate(dmgText, transform.position, Quaternion.identity, transform);
    }
}
