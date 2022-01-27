using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public bool hit;

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

        //if (dmgText != null && currentHp > 0)
        //{
        //    Debug.Log("piew");
        //    ShowDmgText();
        //}
        //hit = true;
    }

    //void ShowDmgText()
    //{
    //    var dmg = Instantiate(dmgText, transform.position, playerModel.transform.rotation, transform);
    //    dmg.GetComponent<TextMesh>().text = damageTaken.ToString();
    //}
}
